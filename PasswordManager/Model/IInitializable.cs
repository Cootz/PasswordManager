namespace PasswordManager.Model
{
    /// <summary>
    /// Provides a mechanism for initializing resources
    /// </summary>
    public interface IInitializable
    {
        public bool IsInitialized();
        public Task Initialize();
    }
}
