namespace PasswordManager.Model.IO;

public static class AppDirectoryManager
{
    private const string appName = "PasswordManager";

    private const string data = "data";

    public static string AppData { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName); } }

    public static string Data { get { return Path.Combine(AppData, data); } }

    public static Task Initialize()
    {
        ensureDirectoryCreated(AppData);
        ensureDirectoryCreated(Data);

        return Task.CompletedTask;
    }

    private static void ensureDirectoryCreated(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}
