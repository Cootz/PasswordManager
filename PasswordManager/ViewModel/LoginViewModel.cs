using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
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

        [ObservableProperty]
        private string password;

        public LoginViewModel(ISecureStorage secureStorage)
        { 
            this.secureStorage = secureStorage;
        }

        [RelayCommand]
        async void Login()
        {
            if (Password == await secureStorage.GetAsync("app-password"))
                Application.Current.MainPage = new AppShell();
        }
    }
}
