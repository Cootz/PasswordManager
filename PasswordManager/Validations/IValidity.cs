namespace PasswordManager.Validations
{
    public interface IValidity
    {
        bool IsValid { get; set; }
        public bool Validate();
    }
}