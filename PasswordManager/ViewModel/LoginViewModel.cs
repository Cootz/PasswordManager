using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Services;
using PasswordManager.Validation;
using PasswordManager.Validation.Rules;
using PasswordManager.View;
using SharpHook;

namespace PasswordManager.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    private readonly INavigationService navigationService;
    private readonly IGlobalHook hook;

    public ValidatableObject<string> Password { get; set; } = new();

    public LoginViewModel(ISecureStorage secureStorage, INavigationService navigation, IGlobalHook globalHook)
    {
        navigationService = navigation;
        hook = globalHook;

        hook.KeyPressed += OnKeyPressed;

        Password.Validations.Add(new LoginPasswordRule(secureStorage)
        {
            ValidationMessage = "Incorrect password"
        });
    }

    private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
    {
        if (e.Data.KeyCode == SharpHook.Native.KeyCode.VcEnter) MainThread.InvokeOnMainThreadAsync(Login);
    }

    [RelayCommand]
    private async Task Login()
    {
        await Task.Run(Password.Validate);

        if (Password.IsValid)
        {
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