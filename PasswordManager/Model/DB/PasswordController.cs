using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model.DB;

public class PasswordController
{
    private IController DB;

    public PasswordController(IController DB)
    {
        this.DB = DB;
    }

    public async Task Initialize()
    {
        await DB.Initialize();
    }

    public async void SavePasswords(Profile[] data)
    {
        foreach (Profile prof in data)
        {
            await DB.Add(prof);
        }
    }

    public async Task Remove(Profile profile) => await DB.Remove(profile); 

    public async void Add(Profile profile) => await DB.Add(profile);

    public IQueryable<Profile> GetProfiles() => DB.Select<Profile>();

}
