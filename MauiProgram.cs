using DexApp.Repository;
using DexApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using DexApp.ViewModels;
using GetYourPlaceApp.Helpers;

namespace DexApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddScoped<IDEXRepository, DEXRepository>();
        
        var app = builder.Build();
        ServiceHelper.Initialize(app.Services);

        return builder.Build();
    }
}