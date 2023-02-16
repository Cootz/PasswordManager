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
        private Service selectedService;

        public AddViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            Services = databaseService.Select<Service>();
            SelectedService = Services.First() ?? Service.DefaultServices.FirstOrDefault();
        }

        //TODO: This look weird, should be done somehow else
        private Profile Validate(Profile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            if (profile.Service == null) throw new ArgumentNullException(nameof(profile));
            if (String.IsNullOrEmpty(profile.Username)) throw new ArgumentException($"Incorrect {nameof(profile.Service)} format.");
            if (String.IsNullOrEmpty(profile.Password)) throw new ArgumentException($"Incorrect {nameof(profile.Service)} format.");

            return profile;
        }

        [RelayCommand]
        async Task AddProfile()
        {
            Profile profile = new Profile()
            {
                Username = Username,
                Password = Password,
                Service = SelectedService
            };

            try
            {
                Validate(profile);

                _databaseService.Add(profile);
            }
            catch (Exception ex)
            {
                
                return;
            }

            await GoBack();
        }

        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
