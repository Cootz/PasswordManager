using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Services
{
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
        /// <returns>User's respose</returns>
        Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel);
    }
}
