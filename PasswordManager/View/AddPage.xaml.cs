using PasswordManager.ViewModel;

namespace PasswordManager.View
{
    public partial class AddPage : ContentPage
    {
        public AddPage(AddViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}