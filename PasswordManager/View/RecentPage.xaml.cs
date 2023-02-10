using PasswordManager.Model.DB;
using PasswordManager.Model.IO;
using PasswordManager.ViewModel;

namespace PasswordManager.View;

public partial class RecentPage : ContentPage
{
	public RecentPage(RecentViewModel vm)
	{
        InitializeComponent();
		BindingContext = vm;
	}
}