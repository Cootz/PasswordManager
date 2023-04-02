﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PasswordManager.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        private ISecureStorage secureStorage;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string passwordConfirmation;

        public RegisterViewModel(ISecureStorage secureStorage)
        {
            this.secureStorage = secureStorage;
        }

        [RelayCommand]
        async void Register()
        {
            if (Password == PasswordConfirmation && Password.Length >= 8)
            {
                await secureStorage.SetAsync("app-password", Password);

#if __MOBILE__
                AppShell.SetFlyoutBehavior(AppShell.Current, FlyoutBehavior.Flyout);
#else
                AppShell.SetFlyoutBehavior(AppShell.Current, FlyoutBehavior.Locked);
#endif
            }
        }
    }
}
