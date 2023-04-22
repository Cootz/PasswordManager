﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Validations
{
    public class ValidatableObject<T> : ObservableObject, IValidity
    {
        private IEnumerable<string> _errors;

        private bool _isValid;

        private T _value;

        public List<IValidationRule<T>> Validations { get; } = new();

        public IEnumerable<string> Errors
        { 
            get => _errors;
            set => SetProperty(ref _errors, value);
        }

        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        public T Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

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
