namespace PasswordManager.Validation
{
    public interface IValidity
    {
        /// <summary>
        /// Defines if object is valid
        /// </summary>
        bool IsValid { get; set; }

        /// <summary>
        /// Validates object
        /// </summary>
        public bool Validate();
    }
}