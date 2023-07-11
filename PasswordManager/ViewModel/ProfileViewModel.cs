using CommunityToolkit.Maui.Core;
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
    async Task CopyToClipboard(string text)
    {
        await Clipboard.Default.SetTextAsync(text);
        var toast = CommunityToolkit.Maui.Alerts.Toast.Make("Password copied");
        await toast.Show();
    }
}