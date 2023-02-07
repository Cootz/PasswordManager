using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model.DB;

internal interface IController: IDisposable
{
    public Task Initialize();
    public IQueryable<T> Select<T>() where T : class;
    public Task Add(Profile profile);
}
