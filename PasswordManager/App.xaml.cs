using PasswordManager.Model.DB;
using PasswordManager.Model.IO;

namespace PasswordManager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}