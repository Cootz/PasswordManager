using Microsoft.Extensions.DependencyInjection;
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
        public static MauiApp CreateMauiApp()
        {
            //TODO: replace with dependency injection
            AppDirectoryManager.Initialize().RunSynchronously();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Setting dependencies for injection
            builder.Services.AddSingleton<IController, RealmController>();
            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddSingleton<RecentPage>();
            builder.Services.AddSingleton<RecentViewModel>();

            builder.Services.AddTransient<AddPage>();
            builder.Services.AddTransient<AddViewModel>();
            
#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}