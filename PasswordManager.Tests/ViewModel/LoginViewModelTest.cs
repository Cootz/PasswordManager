using CommunityToolkit.Mvvm.Input;
using NSubstitute;
using PasswordManager.Services;
using PasswordManager.ViewModel;
using SharpHook;

namespace PasswordManager.Tests.ViewModel;

[TestFixture]
public class LoginViewModelTest
{
    private readonly ISecureStorage secureStorage = Substitute.For<ISecureStorage>();
    private readonly INavigationService navigationService = Substitute.For<INavigationService>();
    private readonly IGlobalHook hook = Substitute.For<IGlobalHook>();
    private const string Password = "TestP@ssw0rd";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        secureStorage.GetAsync("app-password").Returns(Password);
        navigationService.NavigateToAsync(default)
            .ReturnsForAnyArgs(Task.CompletedTask);
    }

    [Test]
    public async Task TestLoginWithCorrectPassword()
    {
        LoginViewModel viewModel = new(secureStorage, navigationService, hook);
        AsyncRelayCommand? command = (AsyncRelayCommand)viewModel.LoginCommand;

        viewModel.Password.Value = Password;

        await command.ExecuteAsync(null);

        await navigationService.Received().NavigateToAsync(Arg.Any<string>());
    }

    [Test]
    public void TestLoginWithIncorrectPassword()
    {
        string? enteredPassword = "wrongPassword";

        LoginViewModel viewModel = new(secureStorage, navigationService, hook);
        IRelayCommand command = viewModel.LoginCommand;

        viewModel.Password.Value = enteredPassword;

        command.Execute(null);

        navigationService.DidNotReceive().NavigateToAsync(Arg.Any<string>());
    }

    [TearDown]
    public void TearDown() => navigationService.ClearReceivedCalls();
}