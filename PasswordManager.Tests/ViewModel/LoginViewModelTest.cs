using NSubstitute;
using PasswordManager.Services;
using PasswordManager.ViewModel;
using SharpHook;

namespace PasswordManager.Tests.ViewModel
{
    [TestFixture]
    public class LoginViewModelTest
    {
        private ISecureStorage secureStorage = Substitute.For<ISecureStorage>();
        private INavigationService navigationService = Substitute.For<INavigationService>();
        private IGlobalHook hook = Substitute.For<IGlobalHook>();
        private const string password = "TestP@ssw0rd";

        private bool pageChanged = false;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            secureStorage.GetAsync("app-password").Returns(password);
            navigationService.NavigateToAsync(default).ReturnsForAnyArgs(Task.CompletedTask).AndDoes((task) => pageChanged = true);
        }

        [Test]
        public void TestLoginWithCorrectPassword()
        {
            string enteredPassword = password;

            LoginViewModel viewModel = new(secureStorage, navigationService, hook);
            var command = viewModel.LoginCommand;

            viewModel.Password = enteredPassword;

            command.Execute(null);

            Assert.That(pageChanged, Is.True);
        }

        [Test]
        public void TestLoginWithIncorrectPassword()
        {
            string enteredPassword = "wrongPassword";

            LoginViewModel viewModel = new(secureStorage, navigationService, hook);
            var command = viewModel.LoginCommand;

            viewModel.Password = enteredPassword;

            command.Execute(null);

            Assert.That(pageChanged, Is.False);
        }

        [TearDown]
        public void TearDown() => pageChanged = false;
    }
}
