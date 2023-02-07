using PasswordManager.Model.DB;
using PasswordManager.Model.IO;

namespace PasswordManager
{
    public partial class App : Application
    {
        public App()
        {
            AppDirectoryManager.Initialize().Wait();

            Task[] Inits = {
                PasswordController.Initialize(),
            };

            InitializeComponent();

            Task.WhenAll(Inits);
            MainPage = new AppShell();
        }
    }
}