using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Validations.Rules
{
    public class PasswordLengthRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value) => value.Length >= 8;
    }
}
