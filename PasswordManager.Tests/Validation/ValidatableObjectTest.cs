using PasswordManager.Tests.TestData;
using PasswordManager.Validation;
using PasswordManager.Validation.Rules;

namespace PasswordManager.Tests.Validation
{
    [TestFixture]
    public class ValidatableObjectTest
    {
        [Test]
        [TestCaseSource(typeof(ValidationDataClass), nameof(ValidationDataClass.RulesTestCases))]
        public bool ValidateWithRules(IValidationRule<object>[] rules)
        {
            ValidatableObject<object> validatableObject = new();

            validatableObject.Validations.AddRange(rules);

            validatableObject.Validate();

            return validatableObject.IsValid;
        }
    }
}
