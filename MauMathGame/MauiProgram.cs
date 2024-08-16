using MauMathGame.Data;
using Microsoft.Extensions.Logging;

namespace MauMathGame
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
                    fonts.AddFont("PermanentMarker-Regular.ttf", "PermanentMarker");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "game.db");

            // will create ONE intance for use in program
            builder.Services.AddSingleton(s => 
                ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
