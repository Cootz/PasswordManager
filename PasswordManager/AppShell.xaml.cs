using PasswordManager.View;

namespace PasswordManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RecentPage), typeof(RecentPage));
        }
    }
}