using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using Realms;

namespace PasswordManager.Services;

/// <summary>
/// Provides interactions with database
/// </summary>
public sealed class DatabaseService : IDisposable
{
    private readonly IController db;
    
    public DatabaseService(IController db) => this.db = db;

    /// <summary>
    /// Save a range of <see cref="ProfileInfo"/>
    /// </summary>
    public async void SavePasswords(IEnumerable<ProfileInfo> data)
    {
        foreach (ProfileInfo prof in data) await db.AddAsync(prof);
    }

    /// <summary>
    /// Deletes entry from database
    /// </summary>
    /// <returns></returns>
    public async Task Remove<T>(T info) where T : IRealmObject
    {
        await db.RemoveAsync(info);
    }

    /// <summary>
    /// Asynchronously wait for the database instance and outstanding objects to get updated to point to the most recent persisted version
    /// </summary>
    /// <returns></returns>
    public async Task Refresh()
    {
        await db.RefreshAsync();
    }

    /// <summary>
    /// Adds entry to database
    /// </summary>
    /// <param name="info"></param>
    public async void Add(IRealmObject info)
    {
        await db.AddAsync(info);
    }

    /// <summary>
    /// Asynchronously provide direct access to realm instance
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public async Task RealmQuery(Func<Realm, Task> action)
    {
        await ((RealmController)db).RealmQueryAsync(action);
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