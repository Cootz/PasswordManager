using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Validation.Rules;
using System.Windows.Input;

namespace PasswordManager.Validation;

/// <summary>
/// Validates <see cref="Value"/> based on <see cref="Validations"/> rules
/// </summary>
public partial class ValidatableObject<T> : ObservableObject, IValidity
{
    [ObservableProperty] private IEnumerable<string> _errors;

    [ObservableProperty] private bool _isValid;

    [ObservableProperty] private T _value = default;

    /// <summary>
    /// Command provider for <see cref="Validate"/> method
    /// </summary>
    public ICommand ValidateCommand { get; }

    /// <summary>
    /// Validations to be applied on the object
    /// </summary>
    public List<IValidationRule<T>> Validations { get; } = new();

    public ValidatableObject()
    {
        IsValid = true;
        Errors = Enumerable.Empty<string>();
        ValidateCommand = new Command(() => Validate());
    }

    /// <summary>
    /// Validates the object to match all the <see cref="Validations"/>
    /// </summary>
    /// <returns></returns>
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