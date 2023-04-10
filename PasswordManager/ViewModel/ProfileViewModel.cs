using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.ViewModel
{
    [QueryProperty(nameof(ProfileInfo), "profile")]
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        ProfileInfo profileInfo;

        public string PageTitle 
        {
            get => $"Profile {ProfileInfo?.Username ?? string.Empty}";
        }

        public ProfileViewModel() { }
    }
}
