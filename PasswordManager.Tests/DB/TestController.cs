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
            ProfileInfo profile = ProfileData.GetTestProfile();

            controller.Add(profile);

            Assert.That(controller.Select<ProfileInfo>().First().Equals(profile), Is.True);
        }

        [Test]
        public void AddMultipleItemsTest()
        {
            ProfileInfo[] profiles = ProfileData.GetTestProfiles();

            controller.SavePasswords(profiles);

            Assert.That(controller.Select<ProfileInfo>().Count(), Is.EqualTo(5));
        }

        [Test]
        public void SelectItemTest()
        {
            ProfileInfo profile = ProfileData.GetTestProfile();
         
            controller.Add(profile);

            Assert.That(controller.Select<ProfileInfo>().Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveItemTest() 
        {
            ProfileInfo profile = ProfileData.GetTestProfile();

            controller.Add(profile);

            Assert.That(controller.Select<ProfileInfo>().Count(), Is.EqualTo(1));

            await controller.Remove(profile);

            Assert.That(controller.Select<ProfileInfo>().Count(), Is.EqualTo(0));
        }

       

    }
}
