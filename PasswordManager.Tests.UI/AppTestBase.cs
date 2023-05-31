using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Platform = Xamarin.UITest.Platform;

#pragma warning disable CS8618

namespace PasswordManager.Tests.UI
{
    [TestFixture(Platform.Android)]
    public class AppTestBase
    {
        protected IApp app;
        protected readonly Platform platform;

        public AppTestBase(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public virtual void SetUp()
        {
            app = AppInitializer.StartApp(platform);
        }

        protected void waitAndAssert(string marked, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
        {
            AppResult[] results = app.WaitForElement(marked, timeoutMessage, timeout, retryFrequency, postTimeout);

            Assert.That(results.Any());
        }

        protected void waitAndAssert(Func<AppQuery, AppQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
        {
            AppResult[] results = app.WaitForElement(query, timeoutMessage, timeout, retryFrequency, postTimeout);

            Assert.That(results.Any());
        }
    }
}
