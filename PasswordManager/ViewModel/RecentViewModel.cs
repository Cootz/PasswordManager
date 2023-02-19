
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.View;

namespace PasswordManager.ViewModel
{
    public partial class RecentViewModel : ObservableObject
    {
        [ObservableProperty]
        IQueryable<ProfileInfo> profiles;

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
        async Task DeleteNote(object sender)
        {
            await db.Remove((ProfileInfo)sender);
        }
    }
}
