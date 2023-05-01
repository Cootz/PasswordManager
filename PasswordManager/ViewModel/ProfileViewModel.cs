using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.ViewModel;

[QueryProperty(nameof(ProfileInfo), "profile")]
public partial class ProfileViewModel : ObservableObject
{
    [ObservableProperty] private ProfileInfo profileInfo;

    public string PageTitle
    {
        get => $"Profile {ProfileInfo?.Username ?? string.Empty}";
    }

    [RelayCommand]
    void CopyToClipboard(string text) => Clipboard.Default.SetTextAsync(text);
}