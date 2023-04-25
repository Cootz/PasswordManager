namespace PasswordManager.Validation.Rules
{
    public class IsNotNullOrEmptyRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value) => !String.IsNullOrEmpty(value);
    }
}