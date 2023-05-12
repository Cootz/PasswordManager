using Xamarin.UITest;

namespace UITestingPractice.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.Debug().InstalledApp("com.companyname.passwordmanager").StartApp();
            }

            return ConfigureApp.iOS.Debug().InstalledApp("com.companyname.passwordmanager").StartApp();
        }
    }
}