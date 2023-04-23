using PasswordManager.Model.Behavior;
using PasswordManager.ViewModel;
using System.Diagnostics;

namespace PasswordManager.View;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}