using Calculator.ViewModel;

namespace Calculator;

public partial class HistoryPage : ContentPage
{

	
	public HistoryPage(HistoryViewModel Historyviewmodel)
	{
		InitializeComponent();
		BindingContext = Historyviewmodel;
	}
}