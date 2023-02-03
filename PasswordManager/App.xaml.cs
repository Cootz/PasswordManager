using PasswordManager.Model.DB;
using PasswordManager.Model.IO;

namespace PasswordManager
{
    public partial class App : Application
    {
        public App()
        {
            Task[] Inits = {
                PasswordController.Initialize(),
                AppDirectoryManager.Initialize()
            };

            InitializeComponent();

            Task.WhenAll(Inits);
            MainPage = new AppShell();
        }
    }
}