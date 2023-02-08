using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model.DB;

/// <summary>
/// Provides database logic. Makes database migration easier
/// </summary>
public interface IController: IDisposable
{
    public Task Initialize();
    public IQueryable<T> Select<T>() where T : class;
    public Task Add(Profile profile);
}
