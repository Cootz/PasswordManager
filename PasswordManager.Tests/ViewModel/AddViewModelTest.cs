using CommunityToolkit.Mvvm.Input;
using NSubstitute;
using PasswordManager.Services;
using PasswordManager.Tests.DB;
using PasswordManager.ViewModel;

namespace PasswordManager.Tests.ViewModel
{
    public class AddViewModelTest : DatabaseTest
    {
        private INavigationService navigationService = Substitute.For<INavigationService>();

        [Test]
        public void ClickOnAddButtonWithValiableProfileTest()
        {
            RunTestWithDatabase((databaseService) =>
            {
                AddViewModel viewModel = new(databaseService, navigationService);

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
                var alertService = Substitute.For<IAlertService>();
                AddViewModel viewModel = new(databaseService, navigationService);
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
                AddViewModel viewModel = new(databaseService, navigationService);
                AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddProfileCommand;

                viewModel.SelectedService = viewModel.Services.First();
                viewModel.Username = "";
                viewModel.Password = "Valid p@ss0wrd";

                Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));
            });
        }
    }
}
