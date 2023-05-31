using Xamarin.UITest;
using Xamarin.UITest.Configuration;
using Platform = Xamarin.UITest.Platform;

namespace PasswordManager.Tests.UI
{
    public class AppInitializer
    {
        private const string debug_apk_file_path =
            @"../../../../PasswordManager/bin/Debug/net7.0-android/com.companyname.passwordmanager-Signed.apk";

        private const string release_apk_file_path =
            @"../../../../PasswordManager/bin/Release/net7.0-android/com.companyname.passwordmanager-Signed.apk";

        private static readonly Lazy<string> apkFilePath = new(() =>
            File.Exists(debug_apk_file_path) ? debug_apk_file_path : release_apk_file_path);

        public static IApp OpenApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.Debug().ApkFile(apkFilePath.Value).StartApp(AppDataMode.DoNotClear);
            }

            return ConfigureApp.iOS.Debug().InstalledApp("com.companyname.passwordmanager").StartApp(AppDataMode.DoNotClear);

        }

        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.Debug().ApkFile(apkFilePath.Value).StartApp();
            }

            return ConfigureApp.iOS.Debug().InstalledApp("com.companyname.passwordmanager").StartApp();
        }
    }
}