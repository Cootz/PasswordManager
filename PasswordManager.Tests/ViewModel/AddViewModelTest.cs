using CommunityToolkit.Mvvm.Input;
using NSubstitute;
using NUnit.Framework;
using PasswordManager.Model.DB;
using PasswordManager.Services;
using PasswordManager.Tests.DB;
using PasswordManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.ViewModel
{
    public class AddViewModelTest
    {
        private AddViewModel viewModel;

        [SetUp]
        public void SetUpViewModel()
        {
            var databaseService = new DatabaseService(new InMemoryRealm());
            databaseService.Initialize().Wait();

            viewModel = new AddViewModel(databaseService);
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

    }
}
