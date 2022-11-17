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

        // List of strings with which to track the previously calculated expressions.
        [ObservableProperty]
        ObservableCollection<string> previousCalculations; 

        // The last expression.
        [ObservableProperty]
        string lastCalculation;

        // The last result (well, ideally, but it doesn't update quickly enough, so it's actually a variant of the last expression)
        [ObservableProperty]
        string lastResult;

        // Adds the last expression and its answer to previousCalculations.
        [RelayCommand]
        void Add()
        {
            string expression = LastResult; // Yes. This is weird.
            string result = Calculator.EvaluatePrefixExpression(Calculator.InfixToPrefix(LastCalculation)); // Yes. This is dumb.


            // ...But it gets the answer, and I'd do a lot of things about this differently if I had the time...
            string output = expression + " = " + result;
            
            // Check to make sure the expression isn't just "0" or null.
            if (string.IsNullOrWhiteSpace(expression) || expression == "0")
                return;
            
            // Assuming it checks out, add it to the history of calculations.
            PreviousCalculations.Add(output);
            
            LastCalculation = output;


            //

        }

        // There for later, but not needed at this point in time.
        [RelayCommand]
        void ClearHistory()
        {
            PreviousCalculations?.Clear();
        }

    }
}
