using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Calculator.Data;
using Calculator.Models;

namespace Calculator.ViewModel
{
    public partial class HistoryViewModel : ObservableObject
    {
        PreviousCalculationsDatabase previousCalculationsDatabase = new();
        public bool dbHistoryIsLoaded = false;

        // List of strings with which to track the previously calculated expressions.
        [ObservableProperty]
        ObservableCollection<string> previousCalculations;

        // The last expression.
        [ObservableProperty]
        string lastCalculation;

        // The last result (well, ideally, but it doesn't update quickly enough, so it's actually a variant of the last expression)
        [ObservableProperty]
        string lastResult;



        public HistoryViewModel()
        {
            PreviousCalculations = new();

            // Load any calculation history from previous sessions.
            LoadHistoryAsync();

            lastCalculation = "0";
            lastResult = "0";
        }

        // Adds the last expression and its answer to previousCalculations.
        [RelayCommand]
        async void Add()
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

            // @TODO: Add a database call to ADD this calculation to the database as a new entry.
            await previousCalculationsDatabase.SaveItemAsync(new PreviousCalculation(0, output));

            LastCalculation = output;

        }

        async void LoadHistoryAsync()
        {
            var previousCalculationsList = await previousCalculationsDatabase.GetItemsAsync();

            foreach (var calculation in previousCalculationsList)
            {
                previousCalculations.Add(calculation.Calculation);
            }
            dbHistoryIsLoaded = true;
        }

        // There for later, but not needed at this point in time.
        [RelayCommand]
        async void ClearHistoryAsync()
        {
            dbHistoryIsLoaded = false;
            await previousCalculationsDatabase.DeleteHistoryAsync();
            PreviousCalculations?.Clear();
            dbHistoryIsLoaded = true;
        }

    }

    
}
