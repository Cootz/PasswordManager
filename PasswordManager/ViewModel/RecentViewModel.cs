using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.View;
using Realms;
using SharpHook;
using SharpHook.Native;

namespace PasswordManager.ViewModel;

public partial class RecentViewModel : ObservableObject
{
    private IQueryable<ProfileInfo> profiles;

    public IQueryable<ProfileInfo> Profiles
    {
        get
        {
            return !string.IsNullOrEmpty(SearchText) && SearchText.Length > 2
                ? profiles.Filter(
                    $"{nameof(ProfileInfo.Service)}.{nameof(ServiceInfo.Name)} CONTAINS[c] $0"
                    + $"|| {nameof(ProfileInfo.Username)} CONTAINS[c] $0", SearchText)
                : profiles;
        }
        set
        {
            profiles = value;
            OnPropertyChanged();
        }
    }

    private string searchText = string.Empty;

    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Profiles));
        }
    }

    private readonly DatabaseService db;
    private readonly INavigationService navigationService;

    public RecentViewModel(DatabaseService databaseService, INavigationService navigationService, IGlobalHook globalHook)
    {
        db = databaseService;
        this.navigationService = navigationService;

        globalHook.KeyReleased += OnKeyReleased;

        Profiles = db.Select<ProfileInfo>();
    }

    private void OnKeyReleased(object sender, KeyboardHookEventArgs e)
    {
        if (e.Data.KeyCode == KeyCode.VcEscape)
            MainThread.BeginInvokeOnMainThread(() => navigationService.PopAsync());
    }

    [RelayCommand]
    private async Task AddNote()
    {
        await navigationService.NavigateToAsync(nameof(AddPage));
    }

    [RelayCommand]
    private async Task DeleteNote(ProfileInfo sender)
    {
        await db.Remove(sender);
    }

    [RelayCommand]
    private async Task ShowNoteInfo(ProfileInfo sender)
    {
        Dictionary<string, object> routeParams = new()
        {
            { "profile", sender }
        };

        await navigationService.NavigateToAsync(nameof(ProfilePage), routeParams);
    }
}