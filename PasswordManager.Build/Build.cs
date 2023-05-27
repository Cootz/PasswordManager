using System.IO;
using System.IO.Compression;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.PowerShell;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.PowerShell.PowerShellTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

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
    CacheIncludePatterns = new[]
    {
        ".nuke/temp",
        "~/.nuget/packages",
        "~/.android/avd/*",
        "~/.android/adb*"
    },
    InvokedTargets = new[] { nameof(UITest) },
    AutoGenerate = false)]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    public readonly Configuration Configuration = Configuration.Debug;

    [Parameter] public readonly string ActionName;

    [Solution]
    public readonly Solution Solution;

    public GitHubActions GitHubActions => GitHubActions.Instance;
    
    public string ExecutionLogFilename => $"TestResults-{ActionName ?? "${{env.ACTION_NAME}}"}";

    public AbsolutePath SourceDirectory => RootDirectory / "PasswordManager";
    public AbsolutePath UnitTestsDirectory => RootDirectory / "PasswordManager.Tests";
    public AbsolutePath UnitTestResultsDirectory => UnitTestsDirectory / "TestResults";
    public AbsolutePath UITestsDirectory => RootDirectory / "PasswordManager.Tests.UI";
    public AbsolutePath UITestsResultsDirectory => UITestsDirectory / "TestResults";

    public AbsolutePath PublishDirectory = RootDirectory / "Publish";

    public Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
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

    public Target Publish => _ => _
        .DependsOn(UnitTest, UITest)
        .Produces(PublishDirectory / "*")
        .Executes(() =>
        {
            Project publishProject = Solution.GetProject("PasswordManager");

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

            zipToPublish(winPublishDirectory, "win.zip");
            zipToPublish(macArmPublishDirectory, "masOS-arm.zip");
            zipToPublish(macIntelPublishDirectory, "masOS-x64.zip");
            
            File.Copy(androidPublishDirectory, PublishDirectory / "PasswordManager.apk");
        });

    private void zipToPublish(AbsolutePath path, string zipFileName) => path.ZipTo(
        PublishDirectory / zipFileName,
        compressionLevel: CompressionLevel.SmallestSize,
        fileMode: FileMode.CreateNew);
}