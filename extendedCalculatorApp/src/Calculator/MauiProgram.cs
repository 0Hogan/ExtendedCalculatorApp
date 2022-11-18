using Calculator.ViewModel;
//using Calculator.Services;

namespace Calculator;

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

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<AboutPage>();
        builder.Services.AddSingleton<AboutViewModel>();

		builder.Services.AddSingleton<AdvancedCalculatorPage>();
		builder.Services.AddSingleton<AdvancedCalculatorViewModel>();

		builder.Services.AddSingleton<HistoryPage>();
		builder.Services.AddSingleton<HistoryViewModel>();

        return builder.Build();
	}
}
