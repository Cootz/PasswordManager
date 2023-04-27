using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.ViewModel
{
    [QueryProperty(nameof(ProfileInfo), "profile")]
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty] private ProfileInfo profileInfo;

        public string PageTitle => $"Profile {ProfileInfo?.Username ?? string.Empty}";

        public ProfileViewModel()
        {
        }
    }
}