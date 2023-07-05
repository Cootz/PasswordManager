using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.ViewModel;

[QueryProperty(nameof(ProfileInfo), "profile")]
public partial class ProfileViewModel : ObservableObject
{
    public const string HIDDEN_PASSWORD = "*****";

    [ObservableProperty] private ProfileInfo profileInfo;

    private bool isPasswordVisible;

    public bool IsPasswordVisible
    {
        get => isPasswordVisible;
        set
        {
            if (isPasswordVisible == value)
                return;

            OnPropertyChanging(nameof(VisiblePassword));
            isPasswordVisible = value;
            OnPropertyChanged(nameof(VisiblePassword));
        }
    }

    public string PageTitle
    {
        get => $"Profile {ProfileInfo?.Username ?? string.Empty}";
    }

    public string VisiblePassword
    {
        get => IsPasswordVisible ? ProfileInfo.Password : HIDDEN_PASSWORD;
    }

    [RelayCommand]
    void CopyToClipboard(string text) => Clipboard.Default.SetTextAsync(text);

    [RelayCommand]
    void ChangePasswordVisibility() => IsPasswordVisible = !IsPasswordVisible;
}