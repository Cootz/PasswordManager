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

            builder.Services.AddSingleton<IController, RealmController>();
            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddSingleton<RecentPage>();
            builder.Services.AddSingleton<RecentViewModel>();

            builder.Services.AddSingleton<ManageServicesPage>();
            builder.Services.AddSingleton<ManageServicesViewModel>();

            builder.Services.AddTransient<AddPage>();
            builder.Services.AddTransient<AddViewModel>();
            
#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}