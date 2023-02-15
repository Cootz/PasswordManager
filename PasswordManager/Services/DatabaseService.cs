using PasswordManager.Model;
using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using Realms;

namespace PasswordManager.Services;

public sealed class DatabaseService : IInitializable, IDisposable
{
    private IController DB;

    private bool isInitialized = false;

    public bool IsInitialized() => isInitialized;

    public DatabaseService(IController DB)
    {
        this.DB = DB;

        //Initialize().RunSynchronously();
    }

    public async Task Initialize()
    {
        await DB.Initialize();

        isInitialized = true;
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

    public IQueryable<T> Select<T>() where T : IRealmObject => DB.Select<T>();

    public void Dispose() => DB.Dispose();
}
