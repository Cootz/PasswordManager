namespace PasswordManager.Services;

public sealed class AlertService : IAlertService
{
    public Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
    }

    public Task<string> ShowPromptAsync(string title, string message, string accept, string cancel)
    {
        return Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel);
    }

    public Task<string> ShowPromptAsync(string title, string message)
    {
        return Application.Current.MainPage.DisplayPromptAsync(title, message);
    }
}