using DrawnUi.Maui.Draw;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace ShadersCarouselDemo
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
                    fonts.AddFont("OpenSans-Regular.ttf", "FontText");
                    fonts.AddFont("OpenSans-Semibold.ttf", "FontTextBold");
                });


            builder.UseDrawnUi(new()
            {
                DesktopWindow = new()
                {
                    Width = 400,
                    Height = 700,
                    //IsFixedSize = true //user cannot resize window
                }
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif


            var culture = CultureInfo.CreateSpecificCulture("en");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.CurrentCulture = culture;

            return builder.Build();
        }
    }
}
