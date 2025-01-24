namespace PasswordManager.Services;

public sealed class AlertService : IAlertService
{
    public Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel) =>
        Application.Current.Windows[0].Page.DisplayAlert(title, message, accept, cancel);

    public Task<bool> ShowConfirmationAsync(string title, string message) =>
        Application.Current.Windows[0].Page.DisplayAlert(title, message, "Yes", "No");

    public Task<string> ShowPromptAsync(string title, string message, string accept, string cancel) =>
        Application.Current.Windows[0].Page.DisplayPromptAsync(title, message, accept, cancel);

    public Task<string> ShowPromptAsync(string title, string message) =>
        Application.Current.Windows[0].Page.DisplayPromptAsync(title, message);
}