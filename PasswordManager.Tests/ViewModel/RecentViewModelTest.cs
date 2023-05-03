using NSubstitute;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.DB;
using PasswordManager.Tests.TestData;
using PasswordManager.ViewModel;
using System.Windows.Input;

namespace PasswordManager.Tests.ViewModel;

public class RecentViewModelTest : DatabaseTest
{
    private readonly INavigationService navigationService = Substitute.For<INavigationService>();

    [Test]
    public void TestListWithEmptySearch()
    {
        RunTestWithDatabase((databaseService) =>
        {
            RecentViewModel viewModel = setupViewModel(databaseService);

            viewModel.SearchText = string.Empty;

            Assert.That(viewModel.Profiles, Is.EquivalentTo(databaseService.Select<ProfileInfo>()));
        });
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    public void TestListWithVeryShortSearch(int length)
    {
        RunTestWithDatabase((databaseService) =>
        {
            RecentViewModel viewModel = setupViewModel(databaseService);

            viewModel.SearchText = new string('t', length);

            Assert.That(viewModel.Profiles, Is.EquivalentTo(databaseService.Select<ProfileInfo>()));
        });
    }

    [Test]
    [TestCase("steam")]
    [TestCase("Steam")]
    public void TestListWithNormalSearchRequest(string searchRequest)
    {
        RunTestWithDatabase((databaseService) =>
        {
            RecentViewModel viewModel = setupViewModel(databaseService);

            viewModel.SearchText = searchRequest;

            ServiceInfo? service = databaseService.Select<ServiceInfo>().First(s => s.Name == "steam");

            Assert.That(viewModel.Profiles,
                Is.EquivalentTo(databaseService.Select<ProfileInfo>().Where(x => x.Service == service)));
        });
    }

    [Test]
    public void TestNavigationOnProfileCardClick()
    {
        RunTestWithDatabase((databaseService) =>
        {
            RecentViewModel viewModel = setupViewModel(databaseService);

            ICommand command = viewModel.ShowNoteInfoCommand;

            Assert.DoesNotThrow(() => command.Execute(databaseService.Select<ProfileInfo>().First()));

            navigationService.Received().NavigateToAsync(Arg.Any<string>(), Arg.Any<Dictionary<string, object>>());
        });
    }

    [Test]
    public void TestProfileDeletion()
    {
        RunTestWithDatabaseAsync(async (databaseService) =>
        {
            RecentViewModel viewModel = setupViewModel(databaseService);

            ICommand command = viewModel.DeleteNoteCommand;

            ProfileInfo? deletedProfile = databaseService.Select<ProfileInfo>().First();

            command.Execute(deletedProfile);

            await databaseService.Refresh();

            await Task.Delay(50);

            Assert.That(deletedProfile, Is.Not.AnyOf(databaseService.Select<ProfileInfo>()));
        });
    }

    [Test]
    public void TestProfileAddition()
    {
        RunTestWithDatabaseAsync(async (databaseService) =>
        {
            RecentViewModel viewModel = setupViewModel(databaseService);

            ICommand command = viewModel.AddNoteCommand;

            command.Execute(null);

            await navigationService.Received().NavigateToAsync(Arg.Any<string>());
        });
    }

    [TearDown]
    public void CleanUp() => navigationService.ClearReceivedCalls();

    private RecentViewModel setupViewModel(DatabaseService databaseService)
    {
        foreach (ServiceInfo? service in ServiceInfo.DefaultServices)
            if (!databaseService.Select<ServiceInfo>().Any(s => s.ID == service.ID))
                databaseService.Add(service);

        ProfileInfo[] testProfiles = ProfileData.GetTestProfiles();

        foreach (ProfileInfo? profile in testProfiles) databaseService.Add(profile);

        return new RecentViewModel(databaseService, navigationService);
    }
}