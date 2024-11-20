using PasswordManager.Services;

namespace PasswordManager;

public partial class App : Application
{
    public static IAlertService AlertService;

    public App(IServiceProvider provider)
    {
        AlertService = provider.GetService<IAlertService>();
        UserAppTheme = provider.GetService<ISettingsService>().CurrentTheme;

        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState activationState) => new(new AppShell());
}