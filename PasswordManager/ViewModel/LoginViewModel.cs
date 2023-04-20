using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Services;
using PasswordManager.View;
using SharpHook;

namespace PasswordManager.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private ISecureStorage secureStorage;
        private INavigationService navigationService;
        private IGlobalHook hook;

        [ObservableProperty]
        private string password;

        public LoginViewModel(ISecureStorage secureStorage, INavigationService navigation, IGlobalHook globalHook)
        {
            this.secureStorage = secureStorage;
            navigationService = navigation;
            hook = globalHook;

            hook.KeyPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
        {
            if (e.Data.KeyCode == SharpHook.Native.KeyCode.VcEnter)
                MainThread.BeginInvokeOnMainThread(Login);
        }

        [RelayCommand]
        async void Login()
        {
            if (Password == await secureStorage.GetAsync("app-password"))
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
}
