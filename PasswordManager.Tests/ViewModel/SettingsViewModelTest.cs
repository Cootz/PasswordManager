using CommunityToolkit.Mvvm.Input;
using NSubstitute;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.DB;
using PasswordManager.Tests.TestData;
using PasswordManager.ViewModel;

namespace PasswordManager.Tests.ViewModel;

public class SettingsViewModelTest : DatabaseTest
{
    private readonly IAlertService alertService = Substitute.For<IAlertService>();

    private const string ServiceName = "Test service";

    [OneTimeSetUp]
    public void OneTimeSetup() => alertService.ShowPromptAsync("", "").ReturnsForAnyArgs(ServiceName);

    [Test]
    public void AddServiceTest()
    {
        RunTestWithDatabase((databaseService) =>
        {
            SettingsViewModel viewModel = new(databaseService, alertService);
            RelayCommand command = (RelayCommand)viewModel.AddServiceCommand;

            Assert.DoesNotThrow(() => command.Execute(null));

            alertService.Received().ShowPromptAsync(Arg.Any<string>(), Arg.Any<string>());
            Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == ServiceName));
        });
    }

    [Test]
    public void RemoveServiceWithoutProfilesTest()
    {
        RunTestWithDatabase((databaseService) =>
        {
            SettingsViewModel viewModel = new(databaseService, alertService);
            RelayCommand<ServiceInfo> command = (RelayCommand<ServiceInfo>)viewModel.RemoveServiceCommand;

            ServiceInfo service = new()
            {
                Name = ServiceName
            };

            databaseService.Add(service);

            Assert.DoesNotThrow(() => command.Execute(service));

            Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == ServiceName), Is.False);
        });
    }

    [Test]
    public void RemoveServiceWithProfilesTest()
    {
        RunTestWithDatabaseAsync(async (databaseService) =>
        {
            SettingsViewModel viewModel = new(databaseService, alertService);
            RelayCommand<ServiceInfo> command = (RelayCommand<ServiceInfo>)viewModel.RemoveServiceCommand;

            ServiceInfo service = new()
            {
                Name = ServiceName
            };

            databaseService.Add(service);

            ProfileInfo[] profileInfos = ProfileData.GetTestProfiles(service);

            foreach (ProfileInfo? profile in profileInfos) databaseService.Add(profile);

            Assert.DoesNotThrow(() => command.Execute(service));

            await databaseService.Refresh();

            await Task.Delay(100);

            Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == ServiceName), Is.False);

            foreach (ProfileInfo? profile in profileInfos)
                Assert.That(databaseService.Select<ProfileInfo>().Any(p => p.ID == profile.ID), Is.False);
        });
    }

    [TearDown]
    public void CleanUp() => alertService.ClearReceivedCalls();
}