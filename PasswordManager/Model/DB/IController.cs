using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model.DB;

/// <summary>
/// Provides database logic. Makes database migration easier
/// </summary>
public interface IController: IInitializable, IDisposable
{
    public IQueryable<T> Select<T>() where T : class;
    public Task Add(Profile profile);
    public Task Remove(Profile profile);
}
