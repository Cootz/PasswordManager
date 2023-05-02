using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using System.Collections.ObjectModel;

namespace PasswordManager.ViewModel;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty] private IQueryable<ServiceInfo> serviceInfos;

    private readonly DatabaseService databaseService;
    private readonly IAlertService alertService;
    private readonly ISettingsService settingsService;

    [ObservableProperty] private ObservableCollection<AppThemeInfo> themes;

    public AppThemeInfo Theme
    {
        get => Themes.First(t => t.Theme == settingsService.CurrentTheme) ;
        set => SetProperty(Theme, value, settingsService, (settings, themeInfo) => settings.CurrentTheme = themeInfo.Theme);
    }

    public SettingsViewModel(DatabaseService db, IAlertService alertService, ISettingsService settingsService)
    {
        databaseService = db;
        this.alertService = alertService;
        this.settingsService = settingsService;

        ServiceInfos = settingsService.ServiceInfos;
        Themes = new()
        {
            new ("Dark", AppTheme.Dark),
            new ("Light", AppTheme.Light),
            new ("System", AppTheme.Unspecified)
        };
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

    public class AppThemeInfo
    {
        public string Name { get; set; }
        public AppTheme Theme { get; set; }

        public AppThemeInfo(string name, AppTheme theme)
        {
            Name = name;
            Theme = theme;
        }
    }
}