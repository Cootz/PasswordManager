namespace PasswordManager.Model
{
    public interface IInitializable
    {
        public bool IsInitialized();
        public Task Initialize();
    }
}
