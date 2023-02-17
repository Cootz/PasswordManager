using CommunityToolkit.Mvvm.Input;
using PasswordManager.Services;
using PasswordManager.Tests.DB;
using PasswordManager.Tests.Services;
using PasswordManager.ViewModel;

namespace PasswordManager.Tests.ViewModel
{
    public class AddViewModelTest
    {
        private AddViewModel viewModel;
        private DatabaseService databaseService;

        [SetUp]
        public void SetUpViewModel()
        {
            databaseService = new DatabaseService(new InMemoryRealm());
            databaseService.Initialize().Wait();

            viewModel = new AddViewModel(databaseService, new MockSuccessfulNavigationService());
        }

        [Test]
        public void ClickOnAddButtonWithValiableProfileTest()
        {
            AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddProfileCommand;

            viewModel.SelectedService = viewModel.Services.First();
            viewModel.Username = "Valid username";
            viewModel.Password = "Valid p@ss0wrd";

            Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(0));
        }

        [Test]
        public void ClickOnAddButtonWithEmptyLoginTest()
        {
            AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddProfileCommand;

            viewModel.SelectedService = viewModel.Services.First();
            viewModel.Username = "";
            viewModel.Password = "Valid p@ss0wrd";

            Assert.ThrowsAsync<ArgumentException>(async () => await command.ExecuteAsync(null));
        }

        [Test]
        public void ClickOnAddButtonWithEmptyPasswordTest()
        {
            AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddProfileCommand;

            viewModel.SelectedService = viewModel.Services.First();
            viewModel.Username = "";
            viewModel.Password = "Valid p@ss0wrd";

            Assert.ThrowsAsync<ArgumentException>(async () => await command.ExecuteAsync(0));
        }

        [TearDown]
        public void TearDown()
        { 
            databaseService.Dispose();

            databaseService = null!;
            viewModel = null!;
        }

    }
}
