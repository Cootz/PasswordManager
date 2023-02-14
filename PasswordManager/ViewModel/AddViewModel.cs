using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.ViewModel
{
    public partial class AddViewModel : ObservableObject
    {
        private DatabaseService _databaseService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;
        
        [ObservableProperty]
        private Service service;

        public AddViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        async Task AddProfile()
        {
            _databaseService.Add(new Profile()
            { 
                Username = Username,
                Password = Password,
                Service= Service
            });

           await GoBack();
        }

        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
