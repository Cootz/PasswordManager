using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Validation;
using PasswordManager.Validation.Rules;

namespace PasswordManager.ViewModel;

public partial class AddViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;
    private readonly INavigationService _navigationService;

    [ObservableProperty] private IQueryable<ServiceInfo> services;

    public ValidatableObject<string> Username { get; } = new();

    public ValidatableObject<string> Password { get; } = new();

    public ValidatableObject<ServiceInfo> SelectedService { get; } = new();

    public AddViewModel(DatabaseService databaseService, INavigationService navigationService)
    {
        _databaseService = databaseService;
        _navigationService = navigationService;

        Services = databaseService.Select<ServiceInfo>();
        SelectedService.Value = Services.First() ?? ServiceInfo.DefaultServices.FirstOrDefault();

        AddValidation();
    }

    private void AddValidation()
    {
        Username.Validations.Add(new IsNotNullOrEmptyRule()
        {
            ValidationMessage = "A username is required"
        });

        Password.Validations.Add(new IsNotNullOrEmptyRule()
        {
            ValidationMessage = "A password is required"
        });

        SelectedService.Validations.Add(new IsNotNullRule<ServiceInfo>()
        {
            ValidationMessage = "A service is required"
        });
    }

    [RelayCommand]
    private async Task AddProfile()
    {
        if (Username.Validate() && Password.Validate() && SelectedService.Validate())
        {
            ProfileInfo profile = new()
            {
                Username = Username.Value,
                Password = Password.Value,
                Service = SelectedService.Value
            };

            _databaseService.Add(profile);

            await GoBack();
        }
    }

    private async Task GoBack()
    {
        await _navigationService.PopAsync();
    }
}