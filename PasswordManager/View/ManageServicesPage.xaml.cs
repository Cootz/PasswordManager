using PasswordManager.ViewModel;

namespace PasswordManager.View;

public partial class ManageServicesPage : ContentPage
{
    public ManageServicesPage(ManageServicesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}