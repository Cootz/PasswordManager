namespace PasswordManager.Validation.Rules
{
    public class PasswordsMatchRule : IValidationRule<(string, string)>
    {
        public string ValidationMessage { get; set; } = "Passwords does not match";

        public bool Check((string, string) value) => value.Item1 == value.Item2;
    }
}