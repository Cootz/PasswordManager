using PasswordManager.ViewModel;

namespace PasswordManager.View;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}