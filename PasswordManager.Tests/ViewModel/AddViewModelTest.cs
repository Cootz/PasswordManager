﻿using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB;
using PasswordManager.Services;
using PasswordManager.Tests.IO;
using PasswordManager.Tests.Services;
using PasswordManager.ViewModel;

namespace PasswordManager.Tests.ViewModel
{
    [TestFixture]
    public class AddViewModelTest
    {
        private AddViewModel viewModel;
        private DatabaseService databaseService;

        [OneTimeSetUp]
        public void SetUpOnce()
        {
            databaseService = new(new RealmController(new TempStorage()));
            databaseService.Initialize().Wait();
        }

        [SetUp]
        public void SetUpViewModel()
        {
            viewModel = new AddViewModel(databaseService, new MockSuccessfulNavigationService());
        }

        [TearDown]
        public void TearDown() => viewModel = null!;
        
        [OneTimeTearDown]
        public void OneTimeTearDown() => databaseService.Dispose();

        [Test]
        public void ClickOnAddButtonWithValiableProfileTest()
        {
            AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddProfileCommand;

            viewModel.SelectedService = viewModel.Services.First();
            viewModel.Username = "Valid username";
            viewModel.Password = "Valid p@ss0wrd";

            Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));
        }

        [Test]
        public void ClickOnAddButtonWithEmptyLoginTest()
        {
            AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddProfileCommand;

            viewModel.SelectedService = viewModel.Services.First();
            viewModel.Username = "";
            viewModel.Password = "Valid p@ss0wrd";

            Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));
        }

        [Test]
        public void ClickOnAddButtonWithEmptyPasswordTest()
        {
            AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddProfileCommand;

            viewModel.SelectedService = viewModel.Services.First();
            viewModel.Username = "";
            viewModel.Password = "Valid p@ss0wrd";

            Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));
        }
    }
}
