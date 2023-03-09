using PasswordManager.View;

namespace PasswordManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

#if __MOBILE__
            this.FlyoutBehavior = FlyoutBehavior.Flyout;
#else
            this.FlyoutBehavior = FlyoutBehavior.Locked;
#endif

            //Register routes
            Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
            Routing.RegisterRoute(nameof(RecentPage), typeof(RecentPage));
        }
    }
}