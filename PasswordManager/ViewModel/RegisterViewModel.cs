using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Services;
using PasswordManager.Validations;
using PasswordManager.Validations.Rules;
using PasswordManager.View;
using SharpHook;

namespace PasswordManager.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly ISecureStorage secureStorage;
        private readonly INavigationService navigationService;
        private readonly IGlobalHook hook;

        private ValidatableObject<(string, string)> matchValidation = new();

        public ValidatableObject<string> Password { get; set; } = new();
        public ValidatableObject<string> PasswordConfirmation { get; set; } = new();

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

            matchValidation.Value = (Password.Value, PasswordConfirmation.Value);
            matchValidation.Validations.Add(new PasswordsMatchRule());

            void AddPasswordValidations(ValidatableObject<string> validatableObject)
            {
                validatableObject.Validations.Add(new IsNotNullOrEmptyRule()
                {
                    ValidationMessage = "A password is required"
                });

                validatableObject.Validations.Add(new PasswordLengthRule());
            }
        }

        private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
        {
            if (e.Data.KeyCode == SharpHook.Native.KeyCode.VcEnter)
                MainThread.BeginInvokeOnMainThread(Register);
        }

        [RelayCommand]
        async void Register()
        {
            if (Password.Validate() && PasswordConfirmation.Validate() && matchValidation.Validate())
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
}
