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

    private const string service_name = "Test service";

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        alertService.ShowPromptAsync("", "").ReturnsForAnyArgs(service_name);

        settingsService.CurrentTheme = AppTheme.Unspecified;
    }

    [SetUp]
    public void SetUp()
    {
        alertService.ShowConfirmationAsync(default, default).ReturnsForAnyArgs(Task.FromResult(true));
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
            Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == service_name));
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
                Name = service_name
            };

            databaseService.Add(service);

            Assert.DoesNotThrow(() => command.Execute(service));

            alertService.Received(1).ShowConfirmationAsync(Arg.Any<string>(), Arg.Any<string>());
            Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == service_name), Is.False);
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
                Name = service_name
            };

            databaseService.Add(service);

            ProfileInfo[] profileInfos = ProfileData.GetTestProfiles(service);

            var profileInfoIds = (from profile in profileInfos select profile.ID).ToArray();

            foreach (ProfileInfo? profile in profileInfos) databaseService.Add(profile);

            Assert.DoesNotThrow(() => command.Execute(service));

            await databaseService.Refresh();

            await alertService.Received(1).ShowConfirmationAsync(Arg.Any<string>(), Arg.Any<string>());
            Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == service_name), Is.False);

            foreach (var profileId in profileInfoIds)
                Assert.That(databaseService.Select<ProfileInfo>().Any(p => p.ID == profileId), Is.False);
        });
    }

    [Test]
    public void RemoveServiceWithCancellationTest()
    {
        RunTestWithDatabase((databaseService) =>
        {
            alertService.ShowConfirmationAsync(default, default).ReturnsForAnyArgs(Task.FromResult(false));

            SettingsViewModel viewModel = setUpViewModel(databaseService);
            RelayCommand<ServiceInfo> command = (RelayCommand<ServiceInfo>)viewModel.RemoveServiceCommand;

            ServiceInfo service = new()
            {
                Name = service_name
            };

            databaseService.Add(service);

            Assert.DoesNotThrow(() => command.Execute(service));

            alertService.Received(1).ShowConfirmationAsync(Arg.Any<string>(), Arg.Any<string>());
            Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == service_name), Is.True);
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