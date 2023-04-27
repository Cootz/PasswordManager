using PasswordManager.ViewModel;

namespace PasswordManager.View
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage(ProfileViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}