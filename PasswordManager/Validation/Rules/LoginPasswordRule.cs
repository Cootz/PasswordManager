using Microsoft.Maui.Storage;
using PasswordManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Validation.Rules
{
    public class LoginPasswordRule : IValidationRule<string>
    {
        private readonly ISecureStorage secureStorage;

        public string ValidationMessage { get; set; }

        public LoginPasswordRule(ISecureStorage secureStorage) 
        {
            this.secureStorage = secureStorage;
        }

        public bool Check(string value) => secureStorage.GetAsync("app-password").Result == value;
    }
}
