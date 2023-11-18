namespace PasswordManager.Validation.Rules;

/// <summary>
/// Represents a validation rule
/// </summary>
/// <typeparam name="T">Type of value to validate</typeparam>
public interface IValidationRule<in T>
{
    /// <summary>
    /// Message shown when value is invalid
    /// </summary>
    string ValidationMessage { get; set; }

    /// <summary>
    /// Checks if <paramref name="value"/> is valid
    /// </summary>
    /// <param name="value">Value to validate</param>
    /// <returns><see langword="true"/> if <paramref name="value"/> is valid, otherwise <see langword="false"/></returns>
    bool Check(T value);
}