using NSubstitute;
using PasswordManager.Tests.Validation;
using PasswordManager.Validation.Rules;

namespace PasswordManager.Tests.TestData;

internal class ValidationData
{
    public static IEnumerable<TestCaseData> RulesTestCases
    {
        get
        {
            yield return new TestCaseData(
                    new object[]
                    {
                        new[]
                        {
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(true)
                        }
                    })
                .Returns(true)
                .SetName($"{nameof(ValidatableObjectTest.ValidateWithRules)}_WithAllValidChecks");

            yield return new TestCaseData(
                    new object[]
                    {
                        new[]
                        {
                            GetRuleThatReturns(false),
                            GetRuleThatReturns(false),
                            GetRuleThatReturns(false),
                            GetRuleThatReturns(false),
                            GetRuleThatReturns(false)
                        }
                    })
                .Returns(false)
                .SetName($"{nameof(ValidatableObjectTest.ValidateWithRules)}_WithAllInvalidChecks");

            yield return new TestCaseData(
                    new object[]
                    {
                        new[]
                        {
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(false),
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(true)
                        }
                    })
                .Returns(false)
                .SetName($"{nameof(ValidatableObjectTest.ValidateWithRules)}_WithOneInvalidCheck");

            yield return new TestCaseData(
                    new object[]
                    {
                        new[]
                        {
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(true),
                            GetRuleThatReturns(false)
                        }
                    })
                .Returns(false)
                .SetName($"{nameof(ValidatableObjectTest.ValidateWithRules)}_WithOneInvalidCheckAtTheEnd");

            yield return new TestCaseData(Enumerable.Empty<IValidationRule<object>>()).Returns(true)
                .SetName($"{nameof(ValidatableObjectTest.ValidateWithRules)}_WithNoRules");
        }
    }

    private static IValidationRule<object> GetRuleThatReturns(bool returnValue)
    {
        IValidationRule<object>? rule = Substitute.For<IValidationRule<object>>();

        rule.Check(default!).ReturnsForAnyArgs(returnValue);

        return rule;
    }
}