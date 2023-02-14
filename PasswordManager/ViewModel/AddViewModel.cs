using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using Realms;
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
        private IQueryable<Service> services;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;
        
        [ObservableProperty]
        private int serviceIndex;

        public AddViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            this.Services = databaseService.Select<Service>();
        }

        [RelayCommand]
        async Task AddProfile()
        {
            _databaseService.Add(new Profile()
            {
                Username = Username,
                Password = Password,
                Service = Services.ElementAt(ServiceIndex)
            });

           await GoBack();
        }

        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
