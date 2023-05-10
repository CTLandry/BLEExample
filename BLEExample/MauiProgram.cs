using CommunityToolkit.Maui;
using BLEExample.Services.Navigation;
using BLEExample.Services.Settings;
using BLEExample.ViewModels;
using BLEExample.Views;
using BLEExample.Services.BLE.SharedImplementation;
using BLEExample.Services.BLE.Interaction;
using BLEExample.Services.Dialog;
using BLEExample.Services.ErrorHandling;

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
        
        mauiAppBuilder.Services.AddTransient<IBLEHandler, BLEHandler>();
        mauiAppBuilder.Services.AddTransient<IDialogService, DialogService>();
        mauiAppBuilder.Services.AddTransient<IErrorReportingService, ErrorReportingService>();



        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<BLEScanViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<BLEScanView>();

        return mauiAppBuilder;
    }
}
