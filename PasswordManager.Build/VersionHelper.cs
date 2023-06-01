using System;
using System.Linq;
using JetBrains.Annotations;

class VersionHelper
{
    public VersionHelper()
    {
    }

    public string GenerateVersion([CanBeNull] string previousVersion)
    {
        var now = DateTime.Now;

        string currentVersion = now.ToString("yyyy.Md");

        string fullCurrentVersion = $"v{currentVersion}.0";

        if (previousVersion is null || previousVersion != fullCurrentVersion)
            return fullCurrentVersion;
        
        int subVersion = int.Parse(previousVersion.Split('.').Last());

        return $"v{currentVersion}.{subVersion + 1}";
    }
}