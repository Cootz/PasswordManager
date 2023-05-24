using System.IO;
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

    [Solution]
    public readonly Solution Solution;

    public GitHubActions GitHubActions => GitHubActions.Instance;

    public string LogFilename => $"TestResults-{GitHubActions.Action}-{GitHubActions.Job}";

    public AbsolutePath SourceDirectory => RootDirectory / "PasswordManager";
    public AbsolutePath TestsDirectory => RootDirectory / "PasswordManager.Tests";

    public Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
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
        .Produces(LogFilename)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile("PasswordManager.Tests")
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetLoggers($"trx;LogFileName={LogFilename}"));
        });

    public Target UITest => _ => _
        .DependsOn(Compile)
        .Produces(LogFilename)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile("PasswordManager.Tests.UI")
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetLoggers($"trx;LogFileName={LogFilename}"));
        });
}
