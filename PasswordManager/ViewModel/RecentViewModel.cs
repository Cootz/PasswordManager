using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model;
using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using PasswordManager.View;
using System.Collections.ObjectModel;

namespace PasswordManager.ViewModel
{
    public partial class RecentViewModel : ObservableObject
    {
        [ObservableProperty]
        private RecentModel model;
        [ObservableProperty]
        IQueryable<Profile> profiles;

        public RecentViewModel()
        {
            Model = new();
            Profiles = PasswordController.GetProfiles();
        }

        [RelayCommand]
        async Task AddNote()
        {
            await Shell.Current.GoToAsync(nameof(AddPage));
        }
    }
}
