using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Tests.DB.Schema
{
    public class ProfileTest
    {
        [Test]
        public void CompareTwoEqualProfilesTest()
        {
            var providedService = new ServiceInfo("steam");

            var Profile = new ProfileInfo { 
                Username= "testUser",
                Password = "testPassword",
                Service = providedService
            };

            var otherProfile = new ProfileInfo
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
            var steamService = new ServiceInfo("steam");

            var Profile = new ProfileInfo
            {
                Username = "testUser",
                Password = "testPassword",
                Service = steamService
            };

            var otherProfile = new ProfileInfo
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
            var steamService = new ServiceInfo("steam");

            var Profile = new ProfileInfo
            {
                Username = "testUser",
                Password = "testPassword",
                Service = steamService
            };

            Assert.That(Profile.Equals(null), Is.False);
        }


        [Test]
        public void CompareNullWithProfileTest()
        {
            var steamService = new ServiceInfo("steam");

            ProfileInfo? Profile = null;

            Assert.That(Profile == new ProfileInfo(), Is.False);
        }

    }
}
