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
        }
    }
}