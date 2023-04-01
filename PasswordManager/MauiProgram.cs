using Microsoft.Extensions.Logging;
using PasswordManager.Model.DB;
using PasswordManager.Model.IO;
using PasswordManager.Services;
using PasswordManager.View;
using PasswordManager.ViewModel;

namespace PasswordManager
{
    public static class MauiProgram
    {
        public const string AppName =
#if DEBUG
            "PasswordManager-debug";
#else
            "PasswordManager";
#endif

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            //Setting dependencies for injection
            builder.Services.AddSingleton<Storage>(x => new AppStorage(Path.Combine(appData, AppName)));

            builder.Services.AddSingleton<INavigationService, NavigationService>();

            builder.Services.AddSingleton<IAlertService, AlertService>();

            builder.Services.AddSingleton(s => new DatabaseService(new RealmController(s.GetService<Storage>(), SecureStorage.Default)));

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton(s => new LoginViewModel(SecureStorage.Default, s.GetService<INavigationService>()));

            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton(s => new RegisterViewModel(SecureStorage.Default));

            builder.Services.AddSingleton<RecentPage>();
            builder.Services.AddSingleton<RecentViewModel>();

            builder.Services.AddTransient<AddPage>();
            builder.Services.AddTransient<AddViewModel>();

            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<SettingsViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}