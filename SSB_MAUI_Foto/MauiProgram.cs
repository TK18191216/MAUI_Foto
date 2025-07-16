using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui; // Add this using directive  

namespace SSB_MAUI_Foto  
{  
    public static class MauiProgram  
    {  
        public static MauiApp CreateMauiApp()  
        {  
            var builder = MauiApp.CreateBuilder();  
            builder  
                .UseMauiApp<App>()  
                .UseMauiCommunityToolkit() // Add this line for CommunityToolkit  
                .ConfigureFonts(fonts =>  
                {  //Hallöchen
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");  
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");  
                });  

#if DEBUG  
            builder.Logging.AddDebug();  
#endif  

            return builder.Build();  
        }  
    }  
}
