using PasswordManager.Tests.TestData;
using PasswordManager.Validation;
using PasswordManager.Validation.Rules;

namespace PasswordManager.Tests.Validation;

[TestFixture]
public class ValidatableObjectTest
{
    [TestCaseSource(typeof(ValidationData), nameof(ValidationData.RulesTestCases))]
    public bool ValidateWithRules(IEnumerable<IValidationRule<object>> rules)
    {
        ValidatableObject<object> validatableObject = new();

        validatableObject.Validations.AddRange(rules);

        validatableObject.Validate();

        return validatableObject.IsValid;
    }
}