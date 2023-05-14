using Xamarin.UITest;
using Xamarin.UITest.Configuration;
using Platform = Xamarin.UITest.Platform;

namespace PasswordManager.Tests.UI
{
    public class AppInitializer
    {
        public static IApp OpenApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.Debug().InstalledApp("com.companyname.passwordmanager").StartApp(AppDataMode.DoNotClear);
            }

            return ConfigureApp.iOS.Debug().InstalledApp("com.companyname.passwordmanager").StartApp(AppDataMode.DoNotClear);

        }

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