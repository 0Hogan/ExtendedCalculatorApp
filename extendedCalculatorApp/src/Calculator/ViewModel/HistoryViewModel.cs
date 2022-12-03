using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;

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

            // @TODO: Add in the functionality to remove all entries from the database.

            PreviousCalculations?.Clear();
            dbHistoryIsLoaded = true;
        }

    }

    public class PreviousCalculation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Calculation { get; set; }

        public PreviousCalculation()
        {
            ID = 0;
            Calculation = "";
        }
        public PreviousCalculation(int iD, string calculation)
        {
            ID = iD;
            Calculation = calculation;
        }
    }

    public class PreviousCalculationsDatabase
    {
        SQLiteAsyncConnection Database;

        public PreviousCalculationsDatabase()
        {}

        // Initialization of the database...
        public async Task Init()
        {
            // If the database is already loaded, go ahead and return.
            if (Database is not null)
                return;

            // Otherwise, open a connection to the database.
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<PreviousCalculation>();
        }

        // Fetch all previous calculations from the database.
        public async Task<List<PreviousCalculation>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<PreviousCalculation>().ToListAsync();
        }

        // Add an item to the database. It's ID should be 0, coming into this function.
        public async Task<int> SaveItemAsync(PreviousCalculation calculation)
        {
            await Init();
            if (calculation.ID != 0)
            {
                return await Database.UpdateAsync(calculation);
            }
            else
            {
                return await Database.InsertAsync(calculation);
            }
        }

        /*
        public async Task<int> DeleteItemAsync(TodoItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
        */

    }
}
