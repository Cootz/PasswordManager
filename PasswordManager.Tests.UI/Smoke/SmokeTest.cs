using PasswordManager.View;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Platform = Xamarin.UITest.Platform;

namespace PasswordManager.Tests.UI.Smoke
{
    public class SmokeTest : AppTestBase
    {
        public SmokeTest(Platform platform) : base(platform)
        {
        }

        [Test]
        public void TestAppLoading() => waitAndAssert("RegisterButton");

        [Test]
        public void TestBasePath()
        {
            waitAndAssert(c => c.Marked("PasswordEntry"));
            app.EnterText("PasswordEntry", "P@ssw0rd");
            app.EnterText("PasswordConfirmation", "P@ssw0rd");

            app.DismissKeyboard();

            app.Screenshot($"Enter and repeated password at the {nameof(RegisterPage)}");

            app.Tap("RegisterButton");

            waitAndAssert("ProfilesCollectionView");
            app.Screenshot($"{nameof(RecentPage)} loaded");

            app.Back();

            app = AppInitializer.OpenApp(platform);

            waitAndAssert("LoginEntry");

            app.EnterText("LoginEntry", "P@ssw0rd");
            app.DismissKeyboard();

            app.Screenshot($"Enter password at the {nameof(LoginPage)}");

            app.Tap("LoginButton");

            waitAndAssert("ProfilesCollectionView");
            app.Screenshot($"{nameof(RecentPage)} loaded");

            app.Tap("Open navigation drawer");
            app.Tap("Settings");

            waitAndAssert("ServicesCollectionView");
            app.Screenshot($"{nameof(SettingsPage)} loaded");

            app.Tap("Open navigation drawer");
            app.Tap("Recent");
            
            waitAndAssert("ProfilesCollectionView");
            app.Screenshot($"Navigated from {nameof(SettingsPage)} to {nameof(RecentPage)} via {nameof(AppShell)}");

            app.Tap("AddButton");

            waitAndAssert("ServicePicker");
            app.Screenshot($"{nameof(AddPage)} loaded");

            app.Back();

            waitAndAssert("ProfilesCollectionView");
            app.Screenshot($"Navigated from {nameof(AddPage)} back to {nameof(RecentPage)}");
        }
    }
}
