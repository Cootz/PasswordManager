using Realms;

namespace PasswordManager.Model.DB;

/// <summary>
/// Provides database logic. Makes database migration easier
/// </summary>
public interface IController: IInitializable, IDisposable
{
    /// <summary>
    /// Select every instance of given class from database
    /// </summary>
    /// <typeparam name="T">Type to search</typeparam>
    public IQueryable<T> Select<T>() where T : IRealmObject;

    /// <summary>
    /// Adds entry to database
    /// </summary>
    public Task Add<T>(T info) where T : IRealmObject;

    /// <summary>
    /// Deletes entry from database
    /// </summary>
    public Task Remove<T>(T info) where T : IRealmObject;

    /// <summary>
    /// Asynchronously wait for the database instance and outstanding objects to get updated to point to the most recent persisted version
    /// </summary>
    /// <returns></returns>
    public Task Refresh();
}
