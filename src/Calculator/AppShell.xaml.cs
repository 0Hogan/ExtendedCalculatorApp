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
        Resources["PrimaryTextColor"] = Colors.Black;


    }

    private void DarkThemeMenuItem_Clicked(object sender, EventArgs e)
	{
        Resources["PrimaryColor"] = Color.FromArgb("56595e"); //"#1460db"; // "#56595e";
        Resources["PageBackgroundColor"] = Colors.Black;
        Resources["PrimaryTextColor"] = Colors.WhiteSmoke;

    }

	private void PinkThemeMenuItem_Clicked(object sender, EventArgs e)
	{
        Resources["PrimaryColor"] = Colors.Pink;
        Resources["PageBackgroundColor"] = Colors.LightPink;
        Resources["PrimaryTextColor"] = Colors.Black;

    }

    private void GreenThemeMenuItem_Clicked(object sender, EventArgs e)
	{
        Resources["PrimaryColor"] = Colors.Green;
        Resources["PageBackgroundColor"] = Colors.DarkGreen;
        Resources["PrimaryTextColor"] = Colors.Black;

    }
}