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

[GitHubActions("Test runners",
    GitHubActionsImage.WindowsLatest, GitHubActionsImage.MacOsLatest,
    OnPushBranches = new[] { "main" },
    OnPullRequestBranches = new[] { "main" },
    InvokedTargets = new[] { nameof(Test) })]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = Configuration.Debug;

    [Solution]
    readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory / "PasswordManager";
    AbsolutePath TestsDirectory => RootDirectory / "PasswordManager.Tests";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            PowerShell(p => p
                .SetCommand($"dotnet workload install maui"));
        });

    /// <remarks>
    /// Allow compile to restore components due to the <see href="https://github.com/dotnet/runtime/issues/62219">bug</see>
    /// </remarks>>
    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
#if DEBUG
                .SetProperty("AndroidSdkDirectory", @"E:\Android\Sdk")
#endif
                );
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

}
