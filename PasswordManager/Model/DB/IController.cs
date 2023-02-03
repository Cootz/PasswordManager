using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model.DB;

internal interface IController
{
    public Task Initialize();
    public IEnumerable<Profile> Select(Func<Profile, bool> predicate);
    public Task Add(Profile profile);
}
