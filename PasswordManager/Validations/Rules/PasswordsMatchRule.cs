using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Validations.Rules
{
    public class PasswordsMatchRule : IValidationRule<(string, string)>
    {
        public string ValidationMessage { get; set; }

        public bool Check((string, string) value) => value.Item1 == value.Item2;
    }
}
