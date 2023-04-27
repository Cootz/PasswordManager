using PasswordManager.View;

namespace PasswordManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Register routes
            Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
            Routing.RegisterRoute(nameof(RecentPage), typeof(RecentPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

            ISecureStorage secureStorage = SecureStorage.Default;

            if (!String.IsNullOrEmpty(secureStorage.GetAsync("app-password").Result))
            {
                GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                GoToAsync($"//{nameof(RegisterPage)}");
            }
        }
    }
}