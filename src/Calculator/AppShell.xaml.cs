namespace Calculator;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
	}

	private void LightThemeMenuItem_Clicked(object sender, EventArgs e)
	{
		Resources["PrimaryColor"] = Colors.White;
        Resources["PageBackgroundColor"] = Colors.LightGrey;

    }

	private void DarkThemeMenuItem_Clicked(object sender, EventArgs e)
	{
        Resources["PrimaryColor"] = Colors.DarkGray;
        Resources["PageBackgroundColor"] = Colors.Black;
    }

	private void PinkThemeMenuItem_Clicked(object sender, EventArgs e)
	{
        Resources["PrimaryColor"] = Colors.Pink;
        Resources["PageBackgroundColor"] = Colors.LightPink;
    }

	private void GreenThemeMenuItem_Clicked(object sender, EventArgs e)
	{
        Resources["PrimaryColor"] = Colors.Green;
        Resources["PageBackgroundColor"] = Colors.DarkGreen;
    }
}