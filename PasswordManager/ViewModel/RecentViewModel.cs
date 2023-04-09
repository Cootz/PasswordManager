using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.View;
using Realms;

namespace PasswordManager.ViewModel
{
    public partial class RecentViewModel : ObservableObject
    {
        private IQueryable<ProfileInfo> profiles;

        public IQueryable<ProfileInfo> Profiles
        {
            get
            {
                if (!String.IsNullOrEmpty(SearchText) && SearchText.Length > 2)
                    return profiles.Filter($"{nameof(ProfileInfo.Service)}.{nameof(ServiceInfo.Name)} CONTAINS[c] $0" +
                    $"|| {nameof(ProfileInfo.Username)} CONTAINS[c] $0", SearchText);
                else
                    return profiles;
            }
            set
            {
                profiles = value;
                OnPropertyChanged(nameof(Profiles));
            }
        }

        string searchText = string.Empty;

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                OnPropertyChanged(nameof(Profiles));
            }
        }

        private DatabaseService db;
        private readonly INavigationService _navigationService;

        public RecentViewModel(DatabaseService databaseService, INavigationService navigationService)
        {
            db = databaseService;
            _navigationService = navigationService;

            Profiles = db.Select<ProfileInfo>();
        }

        [RelayCommand]
        async Task AddNote()
        {
            await _navigationService.NavigateToAsync(nameof(AddPage));
        }

        [RelayCommand]
        async Task DeleteNote(ProfileInfo sender)
        {
            await db.Remove(sender);
        }

        [RelayCommand]
        async Task ShowNoteInfo(ProfileInfo sender)
        {
            var routeParams = new Dictionary<string, object>
            {
                { "profile" , sender }
            };

            await _navigationService.NavigateToAsync(nameof(ProfilePage), routeParams);
        }
    }
}
