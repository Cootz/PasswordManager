using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Validation.Rules
{
    public class IsNotNullOrEmptyRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value) => String.IsNullOrEmpty(value);
    }
}
