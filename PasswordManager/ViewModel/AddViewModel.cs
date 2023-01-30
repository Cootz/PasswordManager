using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.ViewModel
{
    public partial class AddViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;
        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private string service;

        [RelayCommand]
        async Task AddProfile()
        { 


           await GoBack();
        }

        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
