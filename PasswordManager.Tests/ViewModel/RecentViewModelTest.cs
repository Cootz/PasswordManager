﻿using NSubstitute;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.DB;
using PasswordManager.Tests.TestData;
using PasswordManager.ViewModel;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.ViewModel
{
    public class RecentViewModelTest : DatabaseTest
    {
        INavigationService _navigationService = Substitute.For<INavigationService>();

        [Test]
        public void TestListWithEmptySearch() => RunTestWithDatabase((databaseService) => 
        {
            RecentViewModel viewModel = setupViewModel(databaseService);
            
            viewModel.SearchText = string.Empty;

            Assert.That(viewModel.Profiles, Is.EquivalentTo(databaseService.Select<ProfileInfo>()));
        });

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void TestListWithVeryShortSearch(int length) => RunTestWithDatabase((databaseService) =>
        {
            RecentViewModel viewModel = setupViewModel(databaseService);

            viewModel.SearchText = new string('t', length);

            Assert.That(viewModel.Profiles, Is.EquivalentTo(databaseService.Select<ProfileInfo>()));
        });

        [Test]
        [TestCase("steam")]
        [TestCase("Steam")]
        public void TestListWithNormalSearchRequest(string SearchRequest) => RunTestWithDatabase((databaseService) =>
        {
            RecentViewModel viewModel = setupViewModel(databaseService);

            viewModel.SearchText = SearchRequest;

            var profileInfos = databaseService.Select<ProfileInfo>();
            var filteredInfos = profileInfos.Where(x => x.Service == ServiceInfo.DefaultServices[0]);

            Assert.That(viewModel.Profiles, Is.EquivalentTo(filteredInfos));
        });

        private RecentViewModel setupViewModel(DatabaseService databaseService)
        {
            foreach (var service in ServiceInfo.DefaultServices)
                if (!databaseService.Select<ServiceInfo>().Any(s => s.ID == service.ID))
                    databaseService.Add(service);

            var testProfiles = ProfileData.GetTestProfiles();

            foreach (var profile in testProfiles)
                databaseService.Add(profile);

            return new RecentViewModel(databaseService, _navigationService);
        }
    }
}