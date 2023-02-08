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
            var Profile = new Profile { 
                Username= "testUser",
                Password = "testPassword",
                Service = "steam"
            };

            var otherProfile = new Profile
            {
                Username = "testUser",
                Password = "testPassword",
                Service = "steam"
            };

            Assert.That(Profile.Equals(otherProfile), Is.True);
        }

        [Test]
        public void CompareTwoDifferentProfilesTest()
        {
            var Profile = new Profile
            {
                Username = "testUser",
                Password = "testPassword",
                Service = "steam"
            };

            var otherProfile = new Profile
            {
                Username = "diffTestUser",
                Password = "diffTestPassword",
                Service = "steam"
            };

            Assert.That(Profile.Equals(otherProfile), Is.False);
        }

    }
}
