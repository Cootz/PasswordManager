using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Utils;

namespace PasswordManager.ViewModel
{
    public partial class AddViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private IQueryable<Service> services;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;
        
        [ObservableProperty]
        private Service selectedService;

        public AddViewModel(DatabaseService databaseService, INavigationService navigationService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;

            Services = databaseService.Select<Service>();
            SelectedService = Services.First() ?? Service.DefaultServices.FirstOrDefault();
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
                _databaseService.Add(profile.Verify());
            }
            catch { }

            await GoBack();
        }

        async Task GoBack()
        {
            await _navigationService.PopAsync();
        }

    }
}
