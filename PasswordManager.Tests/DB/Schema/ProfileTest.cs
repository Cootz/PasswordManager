using NUnit.Framework;
using PasswordManager.Model.DB.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.DB.Schema
{
    public class ProfileTest
    {
        [Test]
        public void CompareTwoEqualProfilesTest()
        {
            var providedService = new Service("steam");

            var Profile = new Profile { 
                Username= "testUser",
                Password = "testPassword",
                Service = providedService
            };

            var otherProfile = new Profile
            {
                Username = "testUser",
                Password = "testPassword",
                Service = providedService
            };

            Assert.That(Profile.Equals(otherProfile), Is.True);
        }

        [Test]
        public void CompareTwoDifferentProfilesTest()
        {
            var steamService = new Service("steam");

            var Profile = new Profile
            {
                Username = "testUser",
                Password = "testPassword",
                Service = steamService
            };

            var otherProfile = new Profile
            {
                Username = "diffTestUser",
                Password = "diffTestPassword",
                Service = steamService
            };

            Assert.That(Profile.Equals(otherProfile), Is.False);
        }

        [Test]
        public void CompareProfileWithNullTest()
        {
            var steamService = new Service("steam");

            var Profile = new Profile
            {
                Username = "testUser",
                Password = "testPassword",
                Service = steamService
            };

            Assert.That(Profile.Equals(null), Is.False);
        }

    }
}
