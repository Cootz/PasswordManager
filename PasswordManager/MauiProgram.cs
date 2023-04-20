using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.LifecycleEvents;
using PasswordManager.Model.DB;
using PasswordManager.Model.IO;
using PasswordManager.Services;
using PasswordManager.View;
using PasswordManager.ViewModel;
using SharpHook;
using SharpHook.Native;

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
            var globalHook = new TaskPoolGlobalHook();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                    events.AddWindows(windows =>
                    {
                        windows.OnActivated((window, args) =>
                        {
                            if ((args.WindowActivationState == Microsoft.UI.Xaml.WindowActivationState.CodeActivated || args.WindowActivationState == Microsoft.UI.Xaml.WindowActivationState.PointerActivated) && !globalHook.IsRunning)
                                globalHook.RunAsync();
                            else
                                UioHook.Stop();
                        });
                    });
#elif MACCATALYST
                    events.AddiOS(ios =>
                    {
                        ios
                            .OnActivated((app) => { if (!globalHook.IsRunning) globalHook.RunAsync(); })
                            .OnResignActivation((app) => UioHook.Stop());
                    });
#endif
                });

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            //Setting dependencies for injection
            builder.Services.AddSingleton<Storage>(x => new AppStorage(Path.Combine(appData, AppName)));

            builder.Services.AddSingleton<INavigationService, NavigationService>();

            builder.Services.AddSingleton<IAlertService, AlertService>();

            builder.Services.AddSingleton<IGlobalHook, TaskPoolGlobalHook>(s =>
            {
#if WINDOWS || MACCATALYST
                globalHook.RunAsync();
#endif
                return globalHook;
            });

            builder.Services.AddSingleton(s => SecureStorage.Default);

            builder.Services.AddSingleton(s => new DatabaseService(new RealmController(s.GetService<Storage>(), SecureStorage.Default)));

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginViewModel>();

            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<RegisterViewModel>();

            builder.Services.AddSingleton<RecentPage>();
            builder.Services.AddSingleton<RecentViewModel>();

            builder.Services.AddTransient<AddPage>();
            builder.Services.AddTransient<AddViewModel>();

            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<ProfileViewModel>();

            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<SettingsViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}