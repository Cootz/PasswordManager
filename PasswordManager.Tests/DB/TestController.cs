using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;

namespace PasswordManager.Tests.DB
{
    public class TestController
    {
        DatabaseService controller;

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

            Assert.That(controller.Select<Profile>().First().Equals(profile), Is.True);
        }

        [Test]
        public void AddMultipleItemsTest()
        {
            Profile[] profiles = GetTestProfiles();

            controller.SavePasswords(profiles);

            Assert.That(controller.Select<Profile>().Count(), Is.EqualTo(5));
        }

        [Test]
        public void SelectItemTest()
        {
            Profile profile = GetTestProfile();
         
            controller.Add(profile);

            Assert.That(controller.Select<Profile>().Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveItemTest() 
        {
            Profile profile = GetTestProfile();

            controller.Add(profile);

            Assert.That(controller.Select<Profile>().Count(), Is.EqualTo(1));

            await controller.Remove(profile);

            Assert.That(controller.Select<Profile>().Count(), Is.EqualTo(0));
        }

        private Profile GetTestProfile() => new()
        {
            Service = new Service("test service"),
            Username = "test username",
            Password = "test password",
        };

        private Profile[] GetTestProfiles()
        {
            var steam = Service.DefaultServices.Single(s => s.Name == "steam");
            var origin = Service.DefaultServices.Single(s => s.Name == "origin");
            var gog = Service.DefaultServices.Single(s => s.Name == "gog");

            return new Profile[]
            {
                new ()
                {
                    Service = steam,
                    Username = "coo",
                    Password = "P@ssw0rd"
                },
                new ()
                {
                    Service = steam,
                    Username = "Rimo",
                    Password = "Passw0rd"
                },
                new ()
                {
                    Service = steam,
                    Username = "Iro",
                    Password = "Password"
                },
                new ()
                {
                    Service = origin,
                    Username = "Ica",
                    Password = "P@ssword"
                },
                new ()
                {
                    Service = gog,
                    Username = "Tenno",
                    Password = "p@ssword"
                }
            };
        }

    }
}
