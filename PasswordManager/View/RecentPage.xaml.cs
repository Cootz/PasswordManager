using PasswordManager.ViewModel;

namespace PasswordManager.View;

public partial class RecentPage : ContentPage
{
	public RecentPage()
	{
        BindingContext = new RecentViewModel();
        InitializeComponent();
	}
}