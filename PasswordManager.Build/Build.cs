using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitReleaseManager;
using Nuke.Common.Tools.PowerShell;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.PowerShell.PowerShellTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.GitReleaseManager.GitReleaseManagerTasks;

[GitHubActions("Desktop test runner",
    GitHubActionsImage.WindowsLatest, GitHubActionsImage.MacOsLatest,
    OnPushBranches = new[] { "main" },
    OnPullRequestBranches = new[] { "main" },
    CacheIncludePatterns = new[]
    {
        ".nuke/temp", 
        "~/.nuget/packages"
    },
    InvokedTargets = new[] { nameof(UnitTest) },
    AutoGenerate = false)]
[GitHubActions("Mobile test runner",
    GitHubActionsImage.MacOsLatest,
    OnPushBranches = new[] { "main" },
    OnPullRequestBranches = new[] { "main" },
    InvokedTargets = new[] { nameof(UITest) },
    AutoGenerate = false)]
[GitHubActions("Automatic release",
    GitHubActionsImage.MacOsLatest,
    OnPushBranches = new[] { "Release" },
    InvokedTargets = new[] { nameof(Publish) },
    WritePermissions = new[] { 
        GitHubActionsPermissions.Contents, GitHubActionsPermissions.Deployments, 
        GitHubActionsPermissions.RepositoryProjects, GitHubActionsPermissions.Statuses, 
        GitHubActionsPermissions.Packages, GitHubActionsPermissions.Checks},
    EnableGitHubToken = true,
    AutoGenerate = false)]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    public readonly Configuration Configuration = Configuration.Debug;

    [Parameter] public readonly string ActionName;

    [Solution]
    public readonly Solution Solution;

    [LatestGitHubRelease(Build.repository_identifier)] public readonly string LatestGitHubRelease = repository_identifier;
    
    const string repository_identifier = @"Cootz/PasswordManager";

    const string win_release_file_name = "win_x64.zip";
    const string macos_arm_release_file_name = "macOS-arm.zip";
    const string macos_intel_release_file_name = "macOS-x64.zip";
    const string android_release_file_name = "PasswordManager.apk";
    const string passwordmanager_project_name = "PasswordManager";
    public const string DIRECTORY_INDICATOR = "dir";

    public string ExecutionLogFilename => $"TestResults-{ActionName ?? "${{env.ACTION_NAME}}"}";

    public AbsolutePath SourceDirectory => RootDirectory / passwordmanager_project_name;
    public AbsolutePath UnitTestsDirectory => RootDirectory / "PasswordManager.Tests";
    public AbsolutePath UnitTestResultsDirectory => UnitTestsDirectory / "TestResults";
    public AbsolutePath UITestsDirectory => RootDirectory / "PasswordManager.Tests.UI";
    public AbsolutePath UITestsResultsDirectory => UITestsDirectory / "TestResults";

    public AbsolutePath PublishDirectory = RootDirectory / "Publish";
    private readonly ZipHelper zipHelper;
    private readonly VersionHelper versionHelper;

    public Build()
    {
        zipHelper = new ZipHelper(this);
        versionHelper = new VersionHelper();
    }

    public Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            // ReSharper disable once StringLiteralTypo
            RootDirectory.GlobDirectories("[Pp]ublish").ForEach(d => d.DeleteDirectory());

            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
            UnitTestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
            UITestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
        });

    public Target Restore => _ => _
        .Executes(() =>
        {
            foreach (var project in Solution.AllProjects)
            {
                PowerShell(p => p
                    .SetCommand($"dotnet workload restore { Path.GetFileName(project.Path) }")
                    .SetProcessWorkingDirectory(project.Directory));
            }

            DotNetRestore(s => s
                .SetProjectFile(Solution)
            );
        });

    /// <remarks>
    /// Allow compile to restore components due to the <see href="https://github.com/dotnet/runtime/issues/62219">bug</see>
    /// </remarks>>
    public Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
#if DEBUG
                .SetProperty("AndroidSdkDirectory", @"E:\Android\Sdk")
                .SetProperty("EmbedAssembliesIntoApk", "true")
#endif
            );
        });

    public Target UnitTest => _ => _
        .DependsOn(Compile)
        .Produces(UnitTestResultsDirectory / ExecutionLogFilename)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile("PasswordManager.Tests")
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetLoggers($"trx;LogFileName={ExecutionLogFilename}.trx"));
        });

    public Target UITest => _ => _
        .DependsOn(Compile)
        .Produces(UITestsResultsDirectory / ExecutionLogFilename)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile("PasswordManager.Tests.UI")
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetLoggers($"trx;LogFileName={ExecutionLogFilename}.trx"));
        });

    /// <remarks>
    /// We do not depend on <see cref="UITest"/> as it's already executed in a PR to Release branch
    /// </remarks>
    public Target Pack => _ => _
        .DependsOn(Compile, UnitTest)
        .Produces(PublishDirectory)
        .Executes(() =>
        {
            Project publishProject = Solution.GetProject(passwordmanager_project_name);

            var publishFrameworks = publishProject.GetTargetFrameworks()!.Where(f => f != "net7.0");

            foreach (var framework in publishFrameworks)
            {
                DotNetPublish(s => s
                    .SetProject(publishProject)
                    .SetConfiguration(Configuration)
                    .SetFramework(framework)
                    .EnableNoRestore()
                );
            }

            Directory.CreateDirectory(PublishDirectory);

            AbsolutePath winPublishDirectory = SourceDirectory / @"bin\Release\net7.0-windows10.0.19041.0\win10-x64\publish";
            AbsolutePath macArmPublishDirectory = SourceDirectory / @"bin\Release\net7.0-maccatalyst\maccatalyst-arm64\PasswordManager.app";
            AbsolutePath macIntelPublishDirectory = SourceDirectory / @"bin\Release\net7.0-maccatalyst\maccatalyst-x64\PasswordManager.app";
            AbsolutePath androidPublishDirectory = SourceDirectory / @"bin\Release\net7.0-android\publish\com.companyname.passwordmanager-Signed.apk";

            zipHelper.ZipToPublish(winPublishDirectory, win_release_file_name);
            zipHelper.ZipToPublish(macArmPublishDirectory, macos_arm_release_file_name);
            zipHelper.ZipToPublish(macIntelPublishDirectory, macos_intel_release_file_name);
            
            File.Copy(androidPublishDirectory, PublishDirectory / android_release_file_name);
        });

    public Target Publish => _ => _
        .DependsOn(Pack)
        .Executes(() =>
        {
            GitReleaseManagerPublish(c => c
                .SetToken(GitHubActions.Instance.Token)
                .SetTagName(versionHelper.GenerateVersion(LatestGitHubRelease))
                .SetProcessArgumentConfigurator(_ => _
                    .Add("-d")
                    .Add("--generate-notes")
                    .Add("--latest")));
        });
}