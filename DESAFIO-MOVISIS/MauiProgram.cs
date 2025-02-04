using Plugin.LocalNotification;

namespace DESAFIO_MOVISIS;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseLocalNotification()
            .UseMauiCommunityToolkit()            
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IBiometric>(BiometricAuthenticationService.Default);
        builder.Services.AddScoped<UsuarioCasoUso>();
        builder.Services.AddScoped<LembreteCasoUso>();
        builder.Services.AddTransient<IDataStore, DataBaseAsync>();
        builder.Services.AddTransient<AutenticarPage>();
        builder.Services.AddTransient<IniciarPage>();
        builder.Services.AddTransient<CadastrarLembretePage>();
        builder.Services.AddTransient<AutenticarViewModel>();
        builder.Services.AddTransient<IniciarViewModel>();
        builder.Services.AddTransient<CadastrarLembreteViewModel>();
        builder.Services.AddTransient<IHttpClientServico, HttpClientServico>();
        builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
        builder.Services.AddTransient<ILembreteRepositorio, LembreteRepositorio>();

        Env.Load();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

}
