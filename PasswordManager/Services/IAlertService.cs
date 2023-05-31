namespace PasswordManager.Services;

public interface IAlertService
{
    /// <summary>
    /// Show questioning prompt
    /// </summary>
    /// <returns>User's answer</returns>
    Task<string> ShowPromptAsync(string title, string message, string accept, string cancel);

    /// <summary>
    /// Show questioning prompt
    /// </summary>
    /// <returns>User's answer</returns>
    Task<string> ShowPromptAsync(string title, string message);

    /// <summary>
    /// Show confirmation alert
    /// </summary>
    /// <returns>User's response</returns>
    Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel);
    
    /// <summary>
    /// Show confirmation alert
    /// </summary>
    /// <returns>User's response</returns>
    /// <remarks>Show <see cref="ShowConfirmationAsync(string,string,string,string)"/> with default accept = "yes", and cancel = "no"</remarks>
    Task<bool> ShowConfirmationAsync(string title, string message);
}