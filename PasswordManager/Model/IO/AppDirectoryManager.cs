namespace PasswordManager.Model.IO;

public static class AppDirectoryManager
{
    private static string appName = "PasswordManager";
    private static string data = "data";

    public static string AppData { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName); } }
    public static string Data { get { return Path.Combine(AppData, data); } }

    public static async Task Initialize()
    {
        ensureDirectoruCreated(AppData);
        ensureDirectoruCreated(Data);
    }

    private static void ensureDirectoruCreated(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}
