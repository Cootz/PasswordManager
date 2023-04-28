using CommunityToolkit.Mvvm.Input;
using NSubstitute;
using PasswordManager.Services;
using PasswordManager.ViewModel;
using SharpHook;

namespace PasswordManager.Tests.ViewModel;

[TestFixture]
public class RegisterViewModelTest
{
    private ISecureStorage secureStorage = Substitute.For<ISecureStorage>();
    private INavigationService navigationService = Substitute.For<INavigationService>();
    private IGlobalHook hook = Substitute.For<IGlobalHook>();
    private const string password = "TestP@ssw0rd";

    private bool pageChanged = false;
    private string? savedPassword = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        secureStorage.GetAsync("app-password").Returns(Task.FromResult<string>(null!));
        secureStorage.SetAsync(default!, default!)
            .ReturnsForAnyArgs(Task.CompletedTask)
            .AndDoes((info) => savedPassword = (string)info[1]);
        navigationService.NavigateToAsync(default)
            .ReturnsForAnyArgs(Task.CompletedTask)
            .AndDoes((task) => pageChanged = true);
    }

    [Test]
    public void RegisterWithValidPassword()
    {
        string? enteredPassword = password;

        RegisterViewModel viewModel = new(secureStorage, navigationService, hook);
        IRelayCommand? command = viewModel.RegisterCommand;

        viewModel.Password.Value = enteredPassword;
        viewModel.PasswordConfirmation.Value = enteredPassword;

        command.Execute(null);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.Password.IsValid);
            Assert.That(viewModel.PasswordConfirmation.IsValid);
            Assert.That(viewModel.MatchValidation.IsValid);
            Assert.That(savedPassword, Is.EqualTo(password));
            Assert.That(pageChanged, Is.True);
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
            Assert.That(pageChanged, Is.False);
        });
    }

    [Test]
    public void RegisterWithInvalidPasswordConfirmation()
    {
        string? enteredPassword = password;

        RegisterViewModel viewModel = new(secureStorage, navigationService, hook);
        IRelayCommand? command = viewModel.RegisterCommand;

        viewModel.Password.Value = enteredPassword;
        viewModel.PasswordConfirmation.Value = enteredPassword + "123";

        command.Execute(null);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.Password.IsValid, Is.True);
            Assert.That(viewModel.MatchValidation.IsValid, Is.False);
            Assert.That(pageChanged, Is.False);
        });
    }


    [TearDown]
    public void TearDown()
    {
        pageChanged = false;
        savedPassword = null;
    }
}