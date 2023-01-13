using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model.DB;

public static class PasswordController
{
    private static IController DB = new DBController();

    public static async Task Initialize()
    {
        await DB.Initialize();
    }

    public static async void SavePasswords(Profile[] data)
    {
        foreach (Profile prof in data)
        {
            await DB.Add(prof);
        }
    }

    public static async Task<List<Profile>> SearhProfiles(Func<Profile, bool> predicate) => new List<Profile>(await DB.Select(predicate));
}
