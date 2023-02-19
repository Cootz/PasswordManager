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
        private IQueryable<ServiceInfo> services;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;
        
        [ObservableProperty]
        private ServiceInfo selectedService;

        public AddViewModel(DatabaseService databaseService, INavigationService navigationService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;

            Services = databaseService.Select<ServiceInfo>();
            SelectedService = Services.First() ?? ServiceInfo.DefaultServices.FirstOrDefault();
        }

        [RelayCommand]
        async Task AddProfile()
        {
            ProfileInfo profile = new ProfileInfo()
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
