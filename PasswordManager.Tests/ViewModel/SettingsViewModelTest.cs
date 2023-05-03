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
    private readonly ISettingsService settingsService = Substitute.For<ISettingsService>();

    private const string ServiceName = "Test service";

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        alertService.ShowPromptAsync("", "").ReturnsForAnyArgs(ServiceName);

        settingsService.CurrentTheme = AppTheme.Unspecified;
    }

    [Test]
    public void AddServiceTest()
    {
        RunTestWithDatabase((databaseService) =>
        {
            SettingsViewModel viewModel = setUpViewModel(databaseService);
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
            SettingsViewModel viewModel = setUpViewModel(databaseService);
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
            SettingsViewModel viewModel = setUpViewModel(databaseService);
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

    private SettingsViewModel setUpViewModel(DatabaseService databaseService) =>
        new(databaseService, alertService, settingsService);

    [Test]
    public void ChangeAppThemeTest()
    {
        RunTestWithDatabase((databaseService) =>
        {
            SettingsViewModel viewModel = setUpViewModel(databaseService);

            SettingsViewModel.AppThemeInfo darkThemeInfo = viewModel.Themes.First(t => t.Theme == AppTheme.Dark);

            viewModel.CurrentTheme = darkThemeInfo;

            Assert.Multiple(() =>
            {
                Assert.That(settingsService.CurrentTheme == darkThemeInfo.Theme);
                Assert.That(viewModel.CurrentTheme == darkThemeInfo);
            });
        });
    }

    [TearDown]
    public void CleanUp()
    {
        settingsService.CurrentTheme = AppTheme.Unspecified;

        alertService.ClearReceivedCalls();
        settingsService.ClearReceivedCalls();
    }
}