using PasswordManager.View;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

#pragma warning disable CS8618

namespace PasswordManager.Tests.UI.Smoke
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class SmokeTest
    {
        private IApp app;
        private readonly Platform platform;

        public SmokeTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void SetUp()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void TestAppLoading() => WaitAndAssert("Register");

        //[Test]
        public void TestBasePath()
        {
            WaitAndAssert(c => c.Marked("PasswordEntry"));
            app.EnterText(x => x.TextField(), "P@ssw0rd");

            app.Screenshot($"Enter and repeated password at the {nameof(RegisterPage)}");

            app.Tap("RegisterButton");

            WaitAndAssert("ProfilesCollectionView");
            app.Screenshot($"{nameof(RecentPage)} loaded");

            app.Back();

            app = AppInitializer.OpenApp(platform);

            WaitAndAssert("LoginEntry");

            app.EnterText("LoginEntry", "P@ssw0rd");

            app.Screenshot($"Enter password at the {nameof(LoginPage)}");

            app.Tap("LoginButton");

            WaitAndAssert("ProfilesCollectionView");
            app.Screenshot($"{nameof(RecentPage)} loaded");

            app.Tap("Settings");

            WaitAndAssert("ServicesCollectionView");
            app.Screenshot($"{nameof(SettingsPage)} loaded");

            app.Tap("Recent");
            
            WaitAndAssert("ProfilesCollectionView");
            app.Screenshot($"Navigated from {nameof(SettingsPage)} to {nameof(RecentPage)} via {nameof(AppShell)}");

            app.Tap("AddButton");

            WaitAndAssert("ServicePicker");
            app.Screenshot($"{nameof(AddPage)} loaded");

            app.Back();

            WaitAndAssert("ProfilesCollectionView");
            app.Screenshot($"Navigated from {nameof(AddPage)} back to {nameof(RecentPage)}");
        }

        private void WaitAndAssert(string marked, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
        {
            AppResult[] results = app.WaitForElement(marked, timeoutMessage, timeout, retryFrequency, postTimeout);
            
            Assert.That(results.Any());
        }

        private void WaitAndAssert(Func<AppQuery, AppQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
        {
            AppResult[] results = app.WaitForElement(query, timeoutMessage, timeout, retryFrequency, postTimeout);

            Assert.That(results.Any());
        }
    }
}
