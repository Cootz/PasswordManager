using PasswordManager.ViewModel;

namespace PasswordManager.View;

public partial class RecentPage : ContentPage
{
    public RecentPage(RecentViewModel vm)
    {
        BindingContext = vm;

        InitializeComponent();
    }
}