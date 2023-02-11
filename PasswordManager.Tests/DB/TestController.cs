﻿using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.DB
{
    public class TestController
    {
        PasswordController controller;

        [SetUp]
        public void setupController()
        {
            controller = new(new InMemoryRealm());
        }

        [TearDown]
        public void cleanupController()
        {
            controller.Dispose();
            controller = null!;
        }

        [Test]
        public void AddItemTest() 
        {
            Profile profile = GetTestProfile();

            controller.Add(profile);

            Assert.That(controller.GetProfiles().First().Equals(profile), Is.True);
        }

        [Test]
        public void SelectItemTest()
        {
            Profile profile = GetTestProfile();
         
            controller.Add(profile);

            Assert.That(controller.GetProfiles().Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveItemTest() 
        {
            Profile profile = GetTestProfile();

            controller.Add(profile);

            Assert.That(controller.GetProfiles().Count(), Is.EqualTo(1));

            await controller.Remove(profile);

            Assert.That(controller.GetProfiles().Count(), Is.EqualTo(0));
        }

        private Profile GetTestProfile() => new()
        {
            Service = "test service",
            Username = "test username",
            Password = "test password",
        };

        private Profile[] GetTestProfiles()
        {
            return new Profile[]
            {
                new ()
                {
                    Service = "steam",
                    Username = "coo",
                    Password = "P@ssw0rd"
                },
                new ()
                {
                    Service = "steam",
                    Username = "Rimo",
                    Password = "Passw0rd"
                },
                new ()
                {
                    Service = "steam",
                    Username = "Iro",
                    Password = "Password"
                },
                new ()
                {
                    Service = "origin",
                    Username = "Ica",
                    Password = "P@ssword"
                },
                new ()
                {
                    Service = "gog",
                    Username = "Tenno",
                    Password = "p@ssword"
                }
            };
        }

    }
}
