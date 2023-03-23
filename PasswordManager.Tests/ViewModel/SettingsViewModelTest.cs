using CommunityToolkit.Mvvm.Input;
using NSubstitute;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.DB;
using PasswordManager.Tests.TestData;
using PasswordManager.ViewModel;

namespace PasswordManager.Tests.ViewModel
{
    public class SettingsViewModelTest : DatabaseTest
    {
        private IAlertService alertService = Substitute.For<IAlertService>();

        private const string service_name = "Test service";

        [OneTimeSetUp]
        public void OniTimeSetup()
        {
            alertService.ShowPromptAsync("","").ReturnsForAnyArgs(service_name);
        }

        [Test]
        public void AddServiceTest()
        {
            RunTestWithDatabase((databaseService) =>
            {
                SettingsViewModel viewModel = new SettingsViewModel(databaseService, alertService);
                AsyncRelayCommand command = (AsyncRelayCommand)viewModel.AddServiceCommand;

                Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(null));

                Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == service_name));
            });
        }

        [Test]
        public void RemoveServiceWithoutProfilesTest()
        {
            RunTestWithDatabase((databaseService) =>
            {
                SettingsViewModel viewModel = new SettingsViewModel(databaseService, alertService);
                AsyncRelayCommand<ServiceInfo> command = (AsyncRelayCommand<ServiceInfo>)viewModel.RemoveServiceCommand;

                ServiceInfo service = new ServiceInfo()
                {
                    Name = service_name,
                };

                databaseService.Add(service);

                Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(service));

                Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == service_name), Is.False);
            });
        }

        [Test]
        public void RemoveServiceWithProfilesTest()
        {
            RunTestWithDatabase((databaseService) =>
            {
                SettingsViewModel viewModel = new SettingsViewModel(databaseService, alertService);
                AsyncRelayCommand<ServiceInfo> command = (AsyncRelayCommand<ServiceInfo>)viewModel.RemoveServiceCommand;

                ServiceInfo service = new ServiceInfo()
                {
                    Name = service_name,
                };

                databaseService.Add(service);

                ProfileInfo[] profileInfos = ProfileData.GetTestProfiles(service);

                foreach (ProfileInfo profile in profileInfos)
                    databaseService.Add(profile);

                Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync(service));

                Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == service_name), Is.False);
                Assert.That(databaseService.Select<ProfileInfo>().Intersect(profileInfos).Any(), Is.False);
            });
        }
    }
}
