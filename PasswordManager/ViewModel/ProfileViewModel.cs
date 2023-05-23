using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;

namespace PasswordManager.ViewModel;

[QueryProperty(nameof(ProfileInfo), "profile")]
public partial class ProfileViewModel : ObservableObject
{
    [ObservableProperty] private ProfileInfo profileInfo;

    private readonly IToastService toastService;
    private readonly IClipboard clipboard;

    public string PageTitle
    {
        get => $"Profile {ProfileInfo?.Username ?? string.Empty}";
    }

    public ProfileViewModel(IToastService toastService, IClipboard clipboard)
    {
        this.toastService = toastService;
        this.clipboard = clipboard;
    }

    [RelayCommand]
    void CopyToClipboard(string text)
    {
        clipboard.SetTextAsync(text);

        var toast = toastService.Make("Text copied");
        toast.Show();
    }
}