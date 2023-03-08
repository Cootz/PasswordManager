using PasswordManager.Model.DB;
using PasswordManager.Model.IO;
using PasswordManager.Services;

namespace PasswordManager
{
    public partial class App : Application
    {
        public static IAlertService AlertService;

        public App(IServiceProvider provider)
        {
            InitializeComponent();

            AlertService = provider.GetService<IAlertService>();

            MainPage = new AppShell();
        }
    }
}