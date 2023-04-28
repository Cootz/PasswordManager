namespace PasswordManager.Model.IO;

public class AppStorage : Storage
{
    public AppStorage(string path) : base(path)
    {
    }

    public override Storage GetStorageForDirectory(string directory)
    {
        return new AppStorage(Path.Combine(WorkingDirectory, directory));
    }
}