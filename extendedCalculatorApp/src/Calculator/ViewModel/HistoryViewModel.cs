using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Calculator.Models;
using Calculator.Data;
using System.Security.Cryptography;



namespace Calculator.ViewModel
{


    public partial class HistoryViewModel : ObservableObject
    {

        public HistoryViewModel()
        {
            PreviousCalculations = new();
            lastCalculation = "0";
            lastResult = "0";

            Init();
        }


        //private SQLiteAsyncConnection conn;

        CalculationsDatabase conn;
        private async void Init()
        {
            if (conn != null)
            {
                return;


            }

            conn = new CalculationsDatabase();  //create teh database link


            //update the viewmodel data with the database information
            List<DBCalculations> calculations = new List<DBCalculations>();
            calculations = await conn.GetItemsAsync();
            if (calculations.Count > 0)
            {
                for (int i = 0; i < calculations.Count(); i++)
                {
                    PreviousCalculations.Add(calculations[i].Calculation);
                }
            }






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
        async void Add()
        {

            Init();

            string expression = LastResult; // Yes. This is weird.
            string result = Calculator.EvaluatePrefixExpression(Calculator.InfixToPrefix(LastCalculation)); // Yes. This is dumb.


            // ...But it gets the answer, and I'd do a lot of things about this differently if I had the time...
            string output = expression + " = " + result;

            // Check to make sure the expression isn't just "0" or null.
            if (string.IsNullOrWhiteSpace(expression) || expression == "0")
                return;

         


            // update the database with the newly added calculation
            await conn.SaveItemAsync(new DBCalculations { Calculation = output });

            // Assuming it checks out, add it to the history of calculations.
            PreviousCalculations.Add(output);


            //should then add the output to the database



            LastCalculation = output;

        }




        // There for later, but not needed at this point in time.
        [RelayCommand]
        void ClearHistory()
        {



            PreviousCalculations?.Clear();
        }

    }
}