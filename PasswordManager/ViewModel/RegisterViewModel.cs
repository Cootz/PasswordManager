using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        async void Register()
        {
            if (Password == PasswordConfirmation && Password.Length >= 8)
            {
                await secureStorage.SetAsync("app-password", Password);

                Application.Current.MainPage = new AppShell();
            }
        }
    }
}
