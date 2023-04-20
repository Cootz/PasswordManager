using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Services;
using PasswordManager.View;
using SharpHook;

namespace PasswordManager.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        private ISecureStorage secureStorage;
        private INavigationService navigationService;
        private IGlobalHook hook;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string passwordConfirmation;

        public RegisterViewModel(ISecureStorage secureStorage, INavigationService navigationService, IGlobalHook hook)
        {
            this.secureStorage = secureStorage;
            this.navigationService = navigationService;
            this.hook = hook;

            hook.KeyPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
        {
            if (e.Data.KeyCode == SharpHook.Native.KeyCode.VcEnter)
                MainThread.BeginInvokeOnMainThread(Register);
        }

        [RelayCommand]
        async void Register()
        {
            if (Password == PasswordConfirmation && Password.Length >= 8)
            {
                await secureStorage.SetAsync("app-password", Password);

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
