using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Calculator.ViewModel
{
    public partial class HistoryViewModel : ObservableObject
    {
        public HistoryViewModel()
        {
            PreviousCalculations = new();
            lastCalculation = "0";
            lastResult = "0";
        }

        [ObservableProperty]
        ObservableCollection<string> previousCalculations;

        [ObservableProperty]
        string lastCalculation;

        [ObservableProperty]
        string lastResult;


        [RelayCommand]
        void Add()
        {
            string expression = LastResult; // Yes. This is weird.
            //string result = Calculator.EvaluatePrefixExpression(Calculator.InfixToPrefix(LastResult));

            string output = expression;// + " = " + result;
            
            if (string.IsNullOrWhiteSpace(LastResult) || LastResult == "0")
                return;
            PreviousCalculations.Add(LastResult);
            LastCalculation = "";
        }

        [RelayCommand]
        void ClearHistory()
        {
            PreviousCalculations?.Clear();
        }

    }
}
