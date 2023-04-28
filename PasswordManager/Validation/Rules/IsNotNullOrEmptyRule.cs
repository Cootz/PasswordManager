namespace PasswordManager.Validation.Rules;

public class IsNotNullOrEmptyRule : IValidationRule<string>
{
    public string ValidationMessage { get; set; }

    public bool Check(string value)
    {
        return !string.IsNullOrEmpty(value);
    }
}