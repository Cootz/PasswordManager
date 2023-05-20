using Xamarin.UITest;
using Xamarin.UITest.Configuration;
using Platform = Xamarin.UITest.Platform;

namespace PasswordManager.Tests.UI
{
    public class AppInitializer
    {
        private const string apk_file_path =
            @"..\..\..\..\PasswordManager\bin\Debug\net7.0-android\com.companyname.passwordmanager-Signed.apk";

        public static IApp OpenApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.Debug().ApkFile(apk_file_path).StartApp(AppDataMode.DoNotClear);
            }

            return ConfigureApp.iOS.Debug().InstalledApp("com.companyname.passwordmanager").StartApp(AppDataMode.DoNotClear);

        }

        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.Debug().ApkFile(apk_file_path).StartApp();
            }

            return ConfigureApp.iOS.Debug().InstalledApp("com.companyname.passwordmanager").StartApp();
        }
    }
}