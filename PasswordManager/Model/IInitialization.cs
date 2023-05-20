namespace PasswordManager.Model;

/// <summary>
/// Provides a mechanism for initializing resources
/// </summary>
public interface IInitialization
{
    /// <summary>
    /// Show if current instance initialized
    /// </summary>
    public Task Initialization { get; }

    /// <summary>
    /// Load and setup necessary things for this instance to function properly
    /// </summary>
    public Task InitializeAsync();
}