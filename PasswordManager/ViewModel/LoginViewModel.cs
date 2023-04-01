using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using PasswordManager.Services;
using PasswordManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        [RelayCommand]
        async void Login()
        {
            if (Password == await secureStorage.GetAsync("app-password"))
            {
                await Shell.Current.GoToAsync($"///{nameof(RecentPage)}");

#if __MOBILE__
                AppShell.SetFlyoutBehavior(AppShell.Current, FlyoutBehavior.Flyout);
#else
                AppShell.SetFlyoutBehavior(AppShell.Current, FlyoutBehavior.Locked);
#endif
            }

        }
    }
}
