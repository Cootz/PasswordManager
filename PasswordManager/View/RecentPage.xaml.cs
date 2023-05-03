using PasswordManager.ViewModel;

namespace PasswordManager.View;

public partial class RecentPage : ContentPage
{
    public RecentPage(RecentViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        //Swipeview doesn't work in WinUI due to the bug. See: https://github.com/dotnet/maui/issues/8870
#if WINDOWS
        ProfilesCollectionView.ItemTemplate = (DataTemplate)Resources["WindowsSpecific"];
#else
        ProfilesCollectionView.ItemTemplate = (DataTemplate)Resources["General"];
#endif
    }
}