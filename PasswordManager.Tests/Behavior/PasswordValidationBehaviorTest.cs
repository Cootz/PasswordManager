using NSubstitute;
using PasswordManager.Model.Behavior;
using PasswordManager.Validation;

namespace PasswordManager.Tests.Behavior
{
    [TestFixture]
    public class PasswordValidationBehaviorTest
    {
        private bool IsTriggered = false;

        [Test]
        public void ValidationTriggerTest()
        {
            Entry entry = new Entry();

            PasswordValidationBehavior validationBehavior = new PasswordValidationBehavior();

            validationBehavior.Validation = Substitute.For<IValidity>();
            validationBehavior.Validation.Validate().ReturnsForAnyArgs(true).AndDoes((info) => IsTriggered = true);

            entry.Behaviors.Add(validationBehavior);

            entry.Text = "TinyPsw";

            Assert.Multiple(() =>
            {
                Assert.That(IsTriggered);
            });
        }

        [TearDown]
        public void ResetVars()
        {
            IsTriggered = false;
        }
    }
}
