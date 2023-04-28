using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Services;
using PasswordManager.Validation;
using PasswordManager.Validation.Rules;
using PasswordManager.View;
using SharpHook;
using System.ComponentModel;

namespace PasswordManager.ViewModel;

public partial class RegisterViewModel : ObservableObject
{
    private readonly ISecureStorage secureStorage;
    private readonly INavigationService navigationService;
    private readonly IGlobalHook hook;

    public ValidatableObject<(string, string)> MatchValidation { get; } = new();

    public ValidatableObject<string> Password { get; } = new();
    public ValidatableObject<string> PasswordConfirmation { get; } = new();

    public RegisterViewModel(ISecureStorage secureStorage, INavigationService navigationService, IGlobalHook hook)
    {
        this.secureStorage = secureStorage;
        this.navigationService = navigationService;
        this.hook = hook;

        hook.KeyPressed += OnKeyPressed;

        AddValidations();
    }

    private void AddValidations()
    {
        AddPasswordValidations(Password);
        AddPasswordValidations(PasswordConfirmation);

        MatchValidation.Value = (Password.Value, PasswordConfirmation.Value);
        MatchValidation.Validations.Add(new PasswordsMatchRule());

        Password.PropertyChanged += onPasswordPropertyChanged;
        PasswordConfirmation.PropertyChanged += onPasswordPropertyChanged;

        void AddPasswordValidations(ValidatableObject<string> validatableObject)
        {
            validatableObject.Validations.Add(new IsNotNullOrEmptyRule()
            {
                ValidationMessage = "A password is required"
            });

            validatableObject.Validations.Add(new PasswordLengthRule());
        }

        void onPasswordPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Password.Value) || e.PropertyName == nameof(PasswordConfirmation.Value))
                MatchValidation.Value = (Password.Value, PasswordConfirmation.Value);
        }
    }

    private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
    {
        if (e.Data.KeyCode == SharpHook.Native.KeyCode.VcEnter) MainThread.BeginInvokeOnMainThread(Register);
    }

    [RelayCommand]
    private async void Register()
    {
        if (Password.Validate() && PasswordConfirmation.Validate() && MatchValidation.Validate())
        {
            await secureStorage.SetAsync("app-password", Password.Value);

            await navigationService.NavigateToAsync($"//{nameof(RecentPage)}");

#if __MOBILE__
            navigationService.SetFlyoutBehavior(FlyoutBehavior.Flyout);
#else
                navigationService.SetFlyoutBehavior(FlyoutBehavior.Locked);
#endif

            hook.KeyPressed -= OnKeyPressed;
        }
    }
}