using PasswordManager.Model.DB;

namespace PasswordManager
{
    public partial class App : Application
    {
        public App()
        {
            Task dbInit = PasswordController.Initialize();

            InitializeComponent();

            Task.WhenAll(dbInit);
            MainPage = new AppShell();
        }
    }
}