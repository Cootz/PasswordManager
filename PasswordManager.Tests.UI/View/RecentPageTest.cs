using PasswordManager.Model.DB.Schema;
using PasswordManager.Tests.UI.TestData;
using Platform = Xamarin.UITest.Platform;

namespace PasswordManager.Tests.UI.View
{
    public class RecentPageTest : AppTestBase
    {
        private const string test_login = "TestLogin";
        private const string test_password = "TestP@ssw0rd";
        private const string default_service = "steam";

        public RecentPageTest(Platform platform) : base(platform)
        {
        }

        [TestCase(null)]
        [TestCaseSource(typeof(ServiceData), nameof(ServiceData.ServiceTestCases))]
        public void DisplayAddedProfileTest(string? service = null)
        {
            navigateToRecentPage();

            addProfile(test_login, test_password, service);

            waitAndAssert(service ?? default_service);
            waitAndAssert(test_login);
        }

        [Test]
        public void SearchForProfileTest()
        {
            navigateToRecentPage();

            foreach (var service in ServiceInfo.DefaultServices) addProfile(test_login, test_password, service.Name);

            var profileCards = app.WaitForElement(test_login);

            Assert.That(profileCards.Length, Is.EqualTo(ServiceInfo.DefaultServices.Length));

            foreach (var service in ServiceInfo.DefaultServices)
            {
                app.EnterText("SearchBar", service.Name);
                app.DismissKeyboard();

                waitAndAssert(service.Name);

                app.ClearText("SearchBar");
            }
        }

        private void navigateToRecentPage()
        {
            app.WaitForElement(c => c.Marked("PasswordEntry"));
            app.EnterText("PasswordEntry", "P@ssw0rd");
            app.EnterText("PasswordConfirmation", "P@ssw0rd");

            app.DismissKeyboard();
            
            app.Tap("RegisterButton");

            app.WaitForElement("ProfilesCollectionView");
        }

        /// <summary>
        /// Adds profile with given <paramref name="login"/> and <paramref name="password"/>
        /// </summary>
        /// <remarks>
        /// Make sure you call this method from 
        /// </remarks>
        private void addProfile(string login, string password, string? service = null)
        {
            app.Tap("AddButton");

            app.WaitForElement("ServicePicker");

            if (service is not null)
            {
                app.Tap("ServicePicker");
                app.Tap(service);
            }

            app.EnterText("LoginEntry", login);
            app.EnterText("PasswordEntry", password);

            app.DismissKeyboard();

            app.Tap("AddButton");

            app.WaitForElement("ProfilesCollectionView");
        }
    }
}
