using System.Threading.Channels;

namespace PasswordManager.Services;

public sealed class AlertService : IAlertService
{
    public Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel) =>
        Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);

    public Task<bool> ShowConfirmationAsync(string title, string message) => 
        Application.Current.MainPage.DisplayAlert(title, message, "Yes", "No");

    public Task<string> ShowPromptAsync(string title, string message, string accept, string cancel) =>
        Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel);

    public Task<string> ShowPromptAsync(string title, string message) =>
        Application.Current.MainPage.DisplayPromptAsync(title, message);
}