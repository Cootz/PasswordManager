using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace PasswordManager.Services
{
    public class ToastService : IToastService
    {
        public IToast Make(string message, ToastDuration duration = ToastDuration.Short, double textSize = 14) => Toast.Make(message, duration, textSize);
    }
}
