using Calculator.ViewModel;

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

        //builder.Services.AddTransient<AboutPage>();
        //builder.Services.AddTransient<AboutViewModel>();	//I dont think we want to use a transient since it is created and deleted every time navigated to and from



        return builder.Build();
	}
}
