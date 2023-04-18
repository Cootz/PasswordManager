using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using PasswordManager.Services;
using PasswordManager.View;
using SharpHook;

namespace PasswordManager.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private ISecureStorage secureStorage;
        private INavigationService navigationService;

        [ObservableProperty]
        private string password;

        public LoginViewModel(ISecureStorage secureStorage, INavigationService navigation)
        {
            this.secureStorage = secureStorage;
            navigationService = navigation;

            var hook = new TaskPoolGlobalHook();

            hook.KeyPressed += OnKeyPressed;

            hook.RunAsync();
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
            }

        }
    }
}
