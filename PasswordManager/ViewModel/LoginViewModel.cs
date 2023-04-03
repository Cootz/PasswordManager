﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Services;
using PasswordManager.View;

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