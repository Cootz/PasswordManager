using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;

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
            
            Validate(profile);

            _databaseService.Add(profile);

            await GoBack();
        }

        async Task GoBack()
        {
            await _navigationService.PopAsync();
        }

    }
}
