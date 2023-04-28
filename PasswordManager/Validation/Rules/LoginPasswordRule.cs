namespace PasswordManager.Validation.Rules;

public class LoginPasswordRule : IValidationRule<string>
{
    private readonly ISecureStorage secureStorage;

    public string ValidationMessage { get; set; }

    public LoginPasswordRule(ISecureStorage secureStorage)
    {
        this.secureStorage = secureStorage;
    }

    public bool Check(string value)
    {
        return secureStorage.GetAsync("app-password").Result == value;
    }
}