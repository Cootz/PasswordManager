namespace PasswordManager.Validation.Rules;

public class PasswordLengthRule : IValidationRule<string>
{
    private const int MIN_PASSWORD_LENGTH = 8;

    public string ValidationMessage { get; set; } =
        $"Password must be at least {MIN_PASSWORD_LENGTH} characters long";

    public bool Check(string value)
    {
        return value.Length >= 8;
    }
}