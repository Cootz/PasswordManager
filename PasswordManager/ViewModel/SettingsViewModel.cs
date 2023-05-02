using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;

namespace PasswordManager.ViewModel;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty] private IQueryable<ServiceInfo> serviceInfos;

    private readonly DatabaseService databaseService;
    private readonly IAlertService alertService;
    private readonly ISettingsService settingsService;

    public AppTheme Theme
    {
        get => settingsService.CurrentTheme;
        set => SetProperty(settingsService.CurrentTheme, value, settingsService, (settings, theme) => settings.CurrentTheme = theme);
    }

    public SettingsViewModel(DatabaseService db, IAlertService alertService, ISettingsService settingsService)
    {
        databaseService = db;
        this.alertService = alertService;
        this.settingsService = settingsService;

        ServiceInfos = settingsService.ServiceInfos;
    }

    [RelayCommand]
    private async void AddService()
    {
        string response = await alertService.ShowPromptAsync("Service", "Enter service name");

        if (string.IsNullOrEmpty(response)) return;

        ServiceInfo newService = new()
        {
            Name = response
        };

        databaseService.Add(newService);
    }

    [RelayCommand]
    private async void RemoveService(ServiceInfo info)
    {
        await databaseService.RealmQuery(async (realm) =>
        {
            await realm.WriteAsync(() =>
            {
                realm.RemoveRange(info.Profiles);
                realm.Remove(info);
            });
        });
    }
}