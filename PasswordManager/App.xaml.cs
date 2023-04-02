using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Storage;
using PasswordManager.Services;
using PasswordManager.View;
using PasswordManager.ViewModel;

namespace PasswordManager
{
    public partial class App : Application
    {
        public static IAlertService AlertService;

        public App(IServiceProvider provider)
        {
            InitializeComponent();

            AlertService = provider.GetService<IAlertService>();

            //Preinitialize page
            provider.GetRequiredService<RecentPage>();
            provider.GetRequiredService<RecentViewModel>();

            MainPage = new AppShell();
        }
    }
}