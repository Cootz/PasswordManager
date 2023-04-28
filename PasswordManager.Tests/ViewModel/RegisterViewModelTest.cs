using CommunityToolkit.Mvvm.Input;
using NSubstitute;
using PasswordManager.Services;
using PasswordManager.ViewModel;
using SharpHook;

namespace PasswordManager.Tests.ViewModel;

[TestFixture]
public class RegisterViewModelTest
{
    private readonly ISecureStorage secureStorage = Substitute.For<ISecureStorage>();
    private readonly INavigationService navigationService = Substitute.For<INavigationService>();
    private readonly IGlobalHook hook = Substitute.For<IGlobalHook>();
    private const string Password = "TestP@ssw0rd";

    private string? savedPassword;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        secureStorage.GetAsync("app-password").Returns(Task.FromResult<string>(null!));
        secureStorage.SetAsync(default!, default!)
            .ReturnsForAnyArgs(Task.CompletedTask)
            .AndDoes((info) => savedPassword = (string)info[1]);
        navigationService.NavigateToAsync(default)
            .ReturnsForAnyArgs(Task.CompletedTask);
    }

    [Test]
    public void RegisterWithValidPassword()
    {
        RegisterViewModel viewModel = new(secureStorage, navigationService, hook);
        IRelayCommand? command = viewModel.RegisterCommand;

        viewModel.Password.Value = Password;
        viewModel.PasswordConfirmation.Value = Password;

        command.Execute(null);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.Password.IsValid);
            Assert.That(viewModel.PasswordConfirmation.IsValid);
            Assert.That(viewModel.MatchValidation.IsValid);
            Assert.That(savedPassword, Is.EqualTo(Password));
            navigationService.Received().NavigateToAsync(Arg.Any<string>());
        });
    }

    [Test]
    public void RegisterWithShortPassword()
    {
        string? enteredPassword = "tinyPsw";

        RegisterViewModel viewModel = new(secureStorage, navigationService, hook);
        IRelayCommand? command = viewModel.RegisterCommand;

        viewModel.Password.Value = enteredPassword;
        viewModel.PasswordConfirmation.Value = enteredPassword;

        command.Execute(null);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.Password.IsValid, Is.False);
            navigationService.DidNotReceive().NavigateToAsync(Arg.Any<string>());
        });
    }

    [Test]
    public void RegisterWithInvalidPasswordConfirmation()
    {
        string? enteredPassword = Password;

        RegisterViewModel viewModel = new(secureStorage, navigationService, hook);
        IRelayCommand? command = viewModel.RegisterCommand;

        viewModel.Password.Value = enteredPassword;
        viewModel.PasswordConfirmation.Value = enteredPassword + "123";

        command.Execute(null);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.Password.IsValid, Is.True);
            Assert.That(viewModel.MatchValidation.IsValid, Is.False);
            navigationService.DidNotReceive().NavigateToAsync(Arg.Any<string>());
        });
    }


    [TearDown]
    public void TearDown()
    {
        navigationService.ClearReceivedCalls();
        savedPassword = null;
    }
}