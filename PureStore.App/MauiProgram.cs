using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PureStore.App.Services;
using PureStore.App.Services.Interfaces;
using PureStore.App.ViewModels;
using PureStore.App.ViewModels.Desktop;
using PureStore.App.Views.Desktop;
using PureStore.App.Views.Desktop.DetailsPages;

namespace PureStore.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "faBrandRegular");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "faRegular");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "faSolid");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            #region desktop registration
            //--------------pages & viewModels-------------//
            builder.Services.AddScoped<DesktopHome>();
            builder.Services.AddScoped<DesktopHomeViewModel>();

            builder.Services.AddTransient<ViewAppPage>();
            builder.Services.AddTransient<ViewAppPageViewModel>();

            builder.Services.AddScoped<ApplicationsStore>();
            builder.Services.AddScoped<ApplicationStoreViewModel>();

            builder.Services.AddTransient<Downloaded>();
            builder.Services.AddTransient<DownloadPageViewModel>();

            builder.Services.AddTransient<MyApplications>();
            builder.Services.AddSingleton<MyApplicationPageViewModel>();
            //--------------------------------------------//


            //---------------Services----------------------//
            builder.Services.AddSingleton<IStoreService, StoreService>();
            builder.Services.AddSingleton<IDownloadService, DownloadService>();
            builder.Services.AddHttpClient<IUploadingService, UploadingService>(a => a.BaseAddress = new Uri("https://localhost:44313/api/Uploading"));
            //--------------------------------------------//

            #endregion

            return builder.Build();
        }
    }
}