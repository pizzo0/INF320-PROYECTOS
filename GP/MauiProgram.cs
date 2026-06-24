using GP.Views;
using GP.ViewModels;
using GP.Database;
using Microsoft.Extensions.Logging;
using SQLite;
using CommunityToolkit.Maui;

namespace GP;

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

		var dbPath = PathHelper.GetDatabasePath("MyDatabase.db");
		var conn = new SQLiteConnection(dbPath);
		builder.Services.AddSingleton(conn);

		builder.Services.AddSingleton<TransactionRepository>();
		builder.Services.AddSingleton<UserRepository>();
#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddTransient<CreateTransactionViewModel>();
		builder.Services.AddTransient<CreateTransactionPage>();

		builder.Services.AddTransient<SetUserViewModel>();
		builder.Services.AddTransient<SetUserPage>();

		return builder.Build();
	}
}
