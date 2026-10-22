using Microsoft.Extensions.Logging;
using Linqd.App.Services;
using Linqd.App.Data;
namespace Linqd.App;

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
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<AuthenticationService>();
		builder.Services.AddSingleton<UserSessionService>();
		builder.Services.AddSingleton<EventService>();
		builder.Services.AddSingleton<LinqdDatabase>();
		builder.Services.AddSingleton<EventRepository>();
		var app = builder.Build();



		return app;
	}
}
