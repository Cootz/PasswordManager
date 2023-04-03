﻿using CommunityToolkit.Mvvm.Input;
using NSubstitute;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.DB;
using PasswordManager.Tests.TestData;
using PasswordManager.ViewModel;
using Realms;

namespace PasswordManager.Tests.ViewModel
{
    public class SettingsViewModelTest : DatabaseTest
    {
        private IAlertService alertService = Substitute.For<IAlertService>();

        private const string service_name = "Test service";

        [OneTimeSetUp]
        public void OniTimeSetup()
        {
            alertService.ShowPromptAsync("", "").ReturnsForAnyArgs(service_name);
        }

        [Test]
        public void AddServiceTest()
        {
            RunTestWithDatabase((databaseService) =>
            {
                SettingsViewModel viewModel = new SettingsViewModel(databaseService, alertService);
                RelayCommand command = (RelayCommand)viewModel.AddServiceCommand;

                Assert.DoesNotThrow(() => command.Execute(null));

                Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == service_name));
            });
        }

        [Test]
        public void RemoveServiceWithoutProfilesTest()
        {
            RunTestWithDatabase((databaseService) =>
            {
                SettingsViewModel viewModel = new SettingsViewModel(databaseService, alertService);
                RelayCommand<ServiceInfo> command = (RelayCommand<ServiceInfo>)viewModel.RemoveServiceCommand;

                ServiceInfo service = new ServiceInfo()
                {
                    Name = service_name,
                };

                databaseService.Add(service);

                Assert.DoesNotThrow(() => command.Execute(service));

                Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == service_name), Is.False);
            });
        }

        [Test]
        public void RemoveServiceWithProfilesTest()
        {
            RunTestWithDatabaseAsync(async (databaseService) =>
            {
                SettingsViewModel viewModel = new SettingsViewModel(databaseService, alertService);
                RelayCommand<ServiceInfo> command = (RelayCommand<ServiceInfo>)viewModel.RemoveServiceCommand;

                ServiceInfo service = new ServiceInfo()
                {
                    Name = service_name,
                };

                databaseService.Add(service);

                ProfileInfo[] profileInfos = ProfileData.GetTestProfiles(service);

                foreach (ProfileInfo profile in profileInfos)
                    databaseService.Add(profile);

                Assert.DoesNotThrow(() => command.Execute(service));

                await databaseService.Refresh();

                Assert.That(databaseService.Select<ServiceInfo>().Any(s => s.Name == service_name), Is.False);

                Assert.That(databaseService.Select<ProfileInfo>().Concat(profileInfos).Any(), Is.False);
            });
        }
    }
}
