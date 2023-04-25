﻿using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Validation.Rules;

namespace PasswordManager.Validation
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
            IsValid = true;
            Errors = Enumerable.Empty<string>();
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