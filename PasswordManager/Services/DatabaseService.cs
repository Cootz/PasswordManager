using PasswordManager.Model;
using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using Realms;

namespace PasswordManager.Services;

/// <summary>
/// Provides interactions with database
/// </summary>
public sealed class DatabaseService : IInitializable, IDisposable
{
    private IController DB;

    private bool isInitialized = false;

    public bool IsInitialized() => isInitialized;

    public DatabaseService(IController DB)
    {
        this.DB = DB;
    }

    public async Task Initialize()
    {
        await DB.Initialize();

        isInitialized = true;
    }


    /// <summary>
    /// Save a range of <see cref="ProfileInfo"/>
    /// </summary>
    public async void SavePasswords(IEnumerable<ProfileInfo> data)
    {
        foreach (ProfileInfo prof in data)
        {
            await DB.Add(prof);
        }
    }

    /// <summary>
    /// Deletes entry from database
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    public async Task Remove(ProfileInfo profile) => await DB.Remove(profile); 

    /// <summary>
    /// Adds entry to database
    /// </summary>
    /// <param name="profile"></param>
    public async void Add(ProfileInfo profile) => await DB.Add(profile);

    /// <summary>
    /// Select every instance of given class from database
    /// </summary>
    /// <typeparam name="T">Type to search</typeparam>
    public IQueryable<T> Select<T>() where T : IRealmObject => DB.Select<T>();

    public void Dispose() => DB.Dispose();
}
