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
    private readonly IController db;

    private bool isInitialized;

    public bool IsInitialized() => isInitialized;

    public DatabaseService(IController db) => this.db = db;

    public async Task Initialize()
    {
        await db.Initialize();

        isInitialized = true;
    }

    /// <summary>
    /// Save a range of <see cref="ProfileInfo"/>
    /// </summary>
    public async void SavePasswords(IEnumerable<ProfileInfo> data)
    {
        foreach (ProfileInfo prof in data) await db.Add(prof);
    }

    /// <summary>
    /// Deletes entry from database
    /// </summary>
    /// <returns></returns>
    public async Task Remove<T>(T info) where T : IRealmObject
    {
        await db.Remove(info);
    }

    /// <summary>
    /// Asynchronously wait for the database instance and outstanding objects to get updated to point to the most recent persisted version
    /// </summary>
    /// <returns></returns>
    public async Task Refresh()
    {
        await db.Refresh();
    }

    /// <summary>
    /// Adds entry to database
    /// </summary>
    /// <param name="info"></param>
    public async void Add(IRealmObject info)
    {
        await db.Add(info);
    }

    /// <summary>
    /// Asynchronously provide direct access to realm instance
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public async Task RealmQuery(Func<Realm, Task> action)
    {
        await ((RealmController)db).RealmQuery(action);
    }

    /// <summary>
    /// Select every instance of given class from database
    /// </summary>
    /// <typeparam name="T">Type to search</typeparam>
    public IQueryable<T> Select<T>() where T : IRealmObject => db.Select<T>();

    public void Dispose()
    {
        db.Dispose();
    }
}