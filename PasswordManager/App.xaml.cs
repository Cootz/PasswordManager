using Microsoft.Maui.Storage;
using PasswordManager.Services;
using PasswordManager.View;

namespace PasswordManager
{
    public partial class App : Application
    {
        public static IAlertService AlertService;

        public App(IServiceProvider provider)
        {
            InitializeComponent();

            AlertService = provider.GetService<IAlertService>();

            ISecureStorage secureStorage = SecureStorage.Default;

            if (!String.IsNullOrEmpty(secureStorage.GetAsync("app-password").Result))
                MainPage = provider.GetService<LoginPage>();
            else
                MainPage = provider.GetService<RegisterPage>();
        }
    }
}