using CommunityToolkit.Mvvm.Input;
using NSubstitute;
using PasswordManager.Services;
using PasswordManager.ViewModel;
using SharpHook;

namespace PasswordManager.Tests.ViewModel;

[TestFixture]
public class LoginViewModelTest
{
    private readonly ISecureStorage _secureStorage = Substitute.For<ISecureStorage>();
    private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
    private readonly IGlobalHook _hook = Substitute.For<IGlobalHook>();
    private const string Password = "TestP@ssw0rd";

    private bool _pageChanged = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _secureStorage.GetAsync("app-password").Returns(Password);
        _navigationService.NavigateToAsync(default).ReturnsForAnyArgs(Task.CompletedTask)
            .AndDoes((task) => _pageChanged = true);
    }

    [Test]
    public async Task TestLoginWithCorrectPassword()
    {
        var enteredPassword = Password;

        LoginViewModel viewModel = new(_secureStorage, _navigationService, _hook);
        var command = (AsyncRelayCommand)viewModel.LoginCommand;

        viewModel.Password.Value = enteredPassword;

        await command.ExecuteAsync(null);

        Assert.That(_pageChanged, Is.True);
    }

    [Test]
    public void TestLoginWithIncorrectPassword()
    {
        var enteredPassword = "wrongPassword";

        LoginViewModel viewModel = new(_secureStorage, _navigationService, _hook);
        IRelayCommand command = viewModel.LoginCommand;

        viewModel.Password.Value = enteredPassword;

        command.Execute(null);

        Assert.That(_pageChanged, Is.False);
    }

    [TearDown]
    public void TearDown()
    {
        _pageChanged = false;
    }
}