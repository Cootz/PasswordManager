﻿using CommunityToolkit.Mvvm.Input;
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
    private const string correct_password = "TestP@ssw0rd";
    private const string incorrect_password = "wrongPassword";
    private const string warning_message = "Incorrect password";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        secureStorage.GetAsync("app-password").Returns(correct_password);
        navigationService.NavigateToAsync(default)
            .ReturnsForAnyArgs(Task.CompletedTask);
    }

    [Test]
    public async Task TestLoginWithCorrectPassword()
    {
        LoginViewModel viewModel = new(secureStorage, navigationService, hook);
        AsyncRelayCommand command = (AsyncRelayCommand)viewModel.LoginCommand;

        viewModel.Password.Value = correct_password;

        await command.ExecuteAsync(null);

        await navigationService.Received().NavigateToAsync(Arg.Any<string>());
        Assert.That(viewModel.Password.Errors.Any(), Is.False);
    }

    [Test]
    public void TestLoginWithIncorrectPassword()
    {
        LoginViewModel viewModel = new(secureStorage, navigationService, hook);
        IRelayCommand command = viewModel.LoginCommand;

        viewModel.Password.Value = incorrect_password;

        command.Execute(null);

        navigationService.DidNotReceive().NavigateToAsync(Arg.Any<string>());
        Assert.That(viewModel.Password.Errors.First(), Is.EqualTo(warning_message));
    }

    [TearDown]
    public void TearDown() => navigationService.ClearReceivedCalls();
}