using PasswordManager.ViewModel;

namespace PasswordManager.View
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage(RegisterViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}