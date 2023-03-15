using PasswordManager.Model.DB.Schema;
using PasswordManager.Tests.TestData;

namespace PasswordManager.Tests.DB
{
    public class TestController : DatabaseTest
    {
        [Test]
        public void AddItemTest()
        {
            RunTestWithDatabase((controller) =>
            {
                ProfileInfo profile = ProfileData.GetTestProfile();

                controller.Add(profile);

                Assert.That(controller.Select<ProfileInfo>().First().Equals(profile), Is.True);
            });
        }

        [Test]
        public void AddMultipleItemsTest()
        {
            RunTestWithDatabase((controller) =>
            {
                ProfileInfo[] profiles = ProfileData.GetTestProfiles();

                controller.SavePasswords(profiles);

                Assert.That(controller.Select<ProfileInfo>().Count(), Is.EqualTo(5));
            });
        }

        [Test]
        public void SelectItemTest()
        {
            RunTestWithDatabase((controller) =>
            {
                ProfileInfo profile = ProfileData.GetTestProfile();

                controller.Add(profile);

                Assert.That(controller.Select<ProfileInfo>().Count(), Is.EqualTo(1));
            });
        }

        [Test]
        public void RemoveItemTest()
        {
            RunTestWithDatabaseAsync(async (controller) =>
            {
                ProfileInfo profile = ProfileData.GetTestProfile();

                controller.Add(profile);

                Assert.That(controller.Select<ProfileInfo>().Count(), Is.EqualTo(1));

                await controller.Remove(profile);

                Assert.That(controller.Select<ProfileInfo>().Count(), Is.EqualTo(0));
            });
        }
    }
}
