using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Validations
{
    /// <summary>
    /// Validates <see cref="Value"/> based on <see cref="Validations"/> rules
    /// </summary>
    public partial class ValidatableObject<T> : ObservableObject, IValidity
    {
        [ObservableProperty]
        private IEnumerable<string> _errors;

        [ObservableProperty]
        private bool _isValid;

        [ObservableProperty]
        private T _value = default;

        public List<IValidationRule<T>> Validations { get; } = new();

        public ValidatableObject()
        { 
            _isValid = true;
            _errors = Enumerable.Empty<string>();
        }

        public bool Validate()
        {
            Errors = Validations
                ?.Where(v => !v.Check(Value))
                ?.Select(v => v.ValidationMessage)
                ?.ToArray()
                ?? Enumerable.Empty<string>();

            IsValid = !Errors.Any();
            return IsValid;
        }
    }
}
