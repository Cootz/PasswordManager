namespace PasswordManager.Utils;

/// <summary>
/// Contains optimization utils
/// </summary>
public static class OptimizationHelper
{
    /// <summary>
    /// Defines if app is activated
    /// </summary>
    /// <remarks>
    /// Stopping sharphook and running it again causes errors so this will be used instead
    /// </remarks>
    public static bool IsAppActive { get; set; } = true;
}