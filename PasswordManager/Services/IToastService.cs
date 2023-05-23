using CommunityToolkit.Maui.Core;

namespace PasswordManager.Services
{
    public interface IToastService
    {
        IToast Make(string message, ToastDuration duration = ToastDuration.Short, double textSize = 14);
    }
}