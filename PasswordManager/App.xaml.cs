using PasswordManager.Services;
using PasswordManager.View;
using PasswordManager.ViewModel;

namespace PasswordManager;

public partial class App : Application
{
    public static IAlertService AlertService;

    public App(IServiceProvider provider)
    {
        AlertService = provider.GetService<IAlertService>();
        UserAppTheme = provider.GetService<ISettingsService>().CurrentTheme;

        InitializeComponent();

        ////Preinitialize page
        //provider.GetRequiredService<RecentPage>();
        //provider.GetRequiredService<RecentViewModel>();

        MainPage = new AppShell();
    }
}