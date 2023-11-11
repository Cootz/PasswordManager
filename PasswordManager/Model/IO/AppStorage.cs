namespace PasswordManager.Model.IO;

/// <summary>
/// Represents storage for the application
/// </summary>
public class AppStorage : Storage
{
    public AppStorage(string path) : base(path)
    {
    }

    public override Storage GetStorageForDirectory(string directory) =>
        new AppStorage(Path.Combine(WorkingDirectory, directory));
}