using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Model;
using PasswordManager.Model.DB.Schema;
using System.Collections.ObjectModel;

namespace PasswordManager.ViewModel
{
    public partial class RecentViewModel : ObservableObject
    {
        [ObservableProperty]
        private RecentModel model;
        [ObservableProperty]
        ObservableCollection<Profile> profiles;

        public RecentViewModel()
        {
            Model = new();
            Task.Run(async () => Profiles = new(await Model.getProfiles()));           
        }
    }
}
