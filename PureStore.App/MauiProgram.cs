using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            builder.Services.AddTransient<DesktopHome>();
            builder.Services.AddTransient<DesktopHomeViewModel>();

            builder.Services.AddTransient<ViewAppPage>();
            builder.Services.AddTransient<ViewAppPageViewModel>();

            //--------------------------------------------//
            #endregion

            return builder.Build();
        }
    }
}