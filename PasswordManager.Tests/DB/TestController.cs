using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.TestData;

namespace PasswordManager.Tests.DB
{
    [TestFixture]
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
            Profile profile = ProfileData.GetTestProfile();

            controller.Add(profile);

            Assert.That(controller.Select<Profile>().First().Equals(profile), Is.True);
        }

        [Test]
        public void AddMultipleItemsTest()
        {
            Profile[] profiles = ProfileData.GetTestProfiles();

            controller.SavePasswords(profiles);

            Assert.That(controller.Select<Profile>().Count(), Is.EqualTo(5));
        }

        [Test]
        public void SelectItemTest()
        {
            Profile profile = ProfileData.GetTestProfile();
         
            controller.Add(profile);

            Assert.That(controller.Select<Profile>().Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveItemTest() 
        {
            Profile profile = ProfileData.GetTestProfile();

            controller.Add(profile);

            Assert.That(controller.Select<Profile>().Count(), Is.EqualTo(1));

            await controller.Remove(profile);

            Assert.That(controller.Select<Profile>().Count(), Is.EqualTo(0));
        }

       

    }
}
