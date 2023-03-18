using CommunityToolkit.Mvvm.Input;
using Moq;
using PasswordManager.Services;
using PasswordManager.Tests.DB;
using PasswordManager.ViewModel;

namespace PasswordManager.Tests.ViewModel
{
    public class AddViewModelTest : DatabaseTest
    {
        private Mock<INavigationService> navigationService = new();

        [Test]
        public void ClickOnAddButtonWithValiableProfileTest()
        {
            RunTestWithDatabase((databaseService) =>
            {
                AddViewModel viewModel = new(databaseService, navigationService.Object);

                AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddProfileCommand;

                viewModel.SelectedService = viewModel.Services.First();
                viewModel.Username = "Valid username";
                viewModel.Password = "Valid p@ss0wrd";

                Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));
            });
        }

        [Test]
        public void ClickOnAddButtonWithEmptyLoginTest()
        {
            RunTestWithDatabase((databaseService) =>
            {
                AddViewModel viewModel = new(databaseService, navigationService.Object);
                AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddProfileCommand;

                viewModel.SelectedService = viewModel.Services.First();
                viewModel.Username = "";
                viewModel.Password = "Valid p@ss0wrd";

                Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));
            });
        }

        [Test]
        public void ClickOnAddButtonWithEmptyPasswordTest()
        {
            RunTestWithDatabase((databaseService) =>
            {
                AddViewModel viewModel = new(databaseService, navigationService.Object);
                AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddProfileCommand;

                viewModel.SelectedService = viewModel.Services.First();
                viewModel.Username = "";
                viewModel.Password = "Valid p@ss0wrd";

                Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));
            });
        }
    }
}
