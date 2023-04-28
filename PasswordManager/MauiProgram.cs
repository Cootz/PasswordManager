using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using PasswordManager.Model;
using PasswordManager.Model.DB;
using PasswordManager.Model.IO;
using PasswordManager.Services;
using PasswordManager.Utils;
using PasswordManager.View;
using PasswordManager.ViewModel;
using SharpHook;

namespace PasswordManager;

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
        OptimizedTaskPoolGlobalHook globalHook = new(new TaskPoolGlobalHookOptions(4, true));

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
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
                            OptimizationHelper.IsAppActive = args.WindowActivationState !=
                                                             Microsoft.UI.Xaml.WindowActivationState.Deactivated;
                        });
                    });
#elif MACCATALYST
                events.AddiOS(ios =>
                {
                    ios
                        .OnActivated((app) => OptimizationHelper.IsAppActive = true)
                        .OnResignActivation((app) => OptimizationHelper.IsAppActive = false);
                });
#endif
            });

        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        //Setup dependencies for injection
        builder.Services.AddSingleton<Storage>(_ => new AppStorage(Path.Combine(appData, AppName)));

        builder.Services.AddSingleton<INavigationService, NavigationService>();

        builder.Services.AddSingleton<IAlertService, AlertService>();

        builder.Services.AddSingleton<IGlobalHook, OptimizedTaskPoolGlobalHook>(s =>
        {
#if WINDOWS || MACCATALYST
            globalHook.RunAsync();
#endif
            return globalHook;
        });

        builder.Services.AddSingleton(_ => SecureStorage.Default);

        builder.Services.AddSingleton(s =>
            new DatabaseService(new RealmController(s.GetService<Storage>(), SecureStorage.Default)));

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