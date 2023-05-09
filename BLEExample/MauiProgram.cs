using CommunityToolkit.Maui;
using BLEExample.Services.Navigation;
using BLEExample.Services.Settings;
using BLEExample.Services.BLE.Scanning;

namespace BLEExample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    => MauiApp
            .CreateBuilder()
            .UseMauiApp<App>()
            .ConfigureEffects(
                effects =>
                {
                })
            .UseMauiCommunityToolkit()
            .ConfigureFonts(
                fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                })
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterViews()
            .Build();

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
        mauiAppBuilder.Services.AddTransient<IBLEScanningService>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
       //TODO register view models

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        //TODO register views

        return mauiAppBuilder;
    }
}
