
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;
using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Model.IO;
using PasswordManager.Services;
using PasswordManager.View;

namespace PasswordManager.ViewModel
{
    public partial class RecentViewModel : ObservableObject
    {
        [ObservableProperty]
        IQueryable<Profile> profiles;

        private DatabaseService db;

        public RecentViewModel(DatabaseService databaseService)
        {
            db = databaseService;

            Profiles = db.Select<Profile>();
        }

        [RelayCommand]
        async Task AddNote()
        {
            await Shell.Current.GoToAsync(nameof(AddPage));
        }

        [RelayCommand]
        async Task DeleteNote(object sender)
        {
            await db.Remove((Profile)sender);
        }
    }
}
