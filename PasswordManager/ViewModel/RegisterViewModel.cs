using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Services;
using PasswordManager.View;

namespace PasswordManager.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        private ISecureStorage secureStorage;
        private INavigationService navigationService;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string passwordConfirmation;

        public RegisterViewModel(ISecureStorage secureStorage, INavigationService navigationService)
        {
            this.secureStorage = secureStorage;
            this.navigationService = navigationService;
        }

        [RelayCommand]
        async void Register()
        {
            if (Password == PasswordConfirmation && Password.Length >= 8)
            {
                await secureStorage.SetAsync("app-password", Password);

                await navigationService.NavigateToAsync($"//{nameof(RecentPage)}");

#if __MOBILE__
                AppShell.SetFlyoutBehavior(AppShell.Current, FlyoutBehavior.Flyout);
#else
                AppShell.SetFlyoutBehavior(AppShell.Current, FlyoutBehavior.Locked);
#endif
            }
        }
    }
}
