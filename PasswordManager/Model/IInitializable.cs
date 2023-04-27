namespace PasswordManager.Model
{
    /// <summary>
    /// Provides a mechanism for initializing resources
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        /// Show if current instance initialized
        /// </summary>
        public bool IsInitialized();

        /// <summary>
        /// Load and setup necessary things for this instance to function properly
        /// </summary>
        public Task Initialize();
    }
}