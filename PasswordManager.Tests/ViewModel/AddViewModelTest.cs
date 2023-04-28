using CommunityToolkit.Mvvm.Input;
using NSubstitute;
using PasswordManager.Services;
using PasswordManager.Tests.DB;
using PasswordManager.ViewModel;

namespace PasswordManager.Tests.ViewModel;

public class AddViewModelTest : DatabaseTest
{
    private readonly INavigationService navigationService = Substitute.For<INavigationService>();

    [Test]
    public void ClickOnAddButtonWithValuableProfileTest()
    {
        RunTestWithDatabase((databaseService) =>
        {
            AddViewModel viewModel = new(databaseService, navigationService);

            AsyncRelayCommand? command = (AsyncRelayCommand)viewModel.AddProfileCommand;

            viewModel.SelectedService.Value = viewModel.Services.First();
            viewModel.Username.Value = "Valid username";
            viewModel.Password.Value = "Valid p@ss0wrd";

            Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));
            navigationService.Received().PopAsync();
        });
    }

    [Test]
    public void ClickOnAddButtonWithEmptyLoginTest()
    {
        RunTestWithDatabase((databaseService) =>
        {
            AddViewModel viewModel = new(databaseService, navigationService);
            AsyncRelayCommand? command = (AsyncRelayCommand)viewModel.AddProfileCommand;

            viewModel.SelectedService.Value = viewModel.Services.First();
            viewModel.Username.Value = "";
            viewModel.Password.Value = "Valid p@ss0wrd";

            Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));
            navigationService.DidNotReceive().PopAsync();
        });
    }

    [Test]
    public void ClickOnAddButtonWithEmptyPasswordTest()
    {
        RunTestWithDatabase((databaseService) =>
        {
            AddViewModel viewModel = new(databaseService, navigationService);
            AsyncRelayCommand? command = (AsyncRelayCommand)viewModel.AddProfileCommand;

            viewModel.SelectedService.Value = viewModel.Services.First();
            viewModel.Username.Value = "";
            viewModel.Password.Value = "Valid p@ss0wrd";

            Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));
            navigationService.DidNotReceive().PopAsync();
        });
    }

    [TearDown]
    public void CleanUp() => navigationService.ClearReceivedCalls();
}