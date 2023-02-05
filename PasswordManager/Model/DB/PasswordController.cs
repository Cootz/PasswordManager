using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model.DB;

public static class PasswordController
{
    private static IController DB = new RealmController();

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

    public static async void Add(Profile profile) => await DB.Add(profile);

    public static IQueryable<Profile> GetProfiles() => DB.Select<Profile>();

}
