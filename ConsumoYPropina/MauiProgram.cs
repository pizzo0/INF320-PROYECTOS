using ConsumoYPropina.ViewModels;
using ConsumoYPropina.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ConsumoYPropina;

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
				fonts.AddFont("GoogleSansCode-Regular.ttf", "GoogleSansCodeRegular");
				fonts.AddFont("GoogleSansCode-Semibold.ttf", "GoogleSansCodeSemibold");
				fonts.AddFont("MaterialSymbols.ttf", "MaterialSymbols");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<MainPage>();

		return builder.Build();
	}
}
