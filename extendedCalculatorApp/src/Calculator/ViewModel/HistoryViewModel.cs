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

        public HistoryViewModel()
        {
            PreviousCalculations = new();

            previousCalculationsDatabase.Init();
            // @TODO: Add a database call to load all of the previous calculations into PreviousCalculations...

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

            // @TODO: Add a database call to ADD this calculation to the database as a new entry.

            LastCalculation = output;

        }

        // There for later, but not needed at this point in time.
        [RelayCommand]
        void ClearHistory()
        {

            // @TODO: Add in the functionality to remove all entries from the database.

            PreviousCalculations?.Clear();
        }

    }

    public class PreviousCalculation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Calculation { get; set; }
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

        /* As of yet unimplemented database accessors.
        public async Task<List<TodoItem>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<TodoItem>().ToListAsync();
        }

        public async Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<TodoItem>().Where(t => t.Done).ToListAsync();

            // SQL queries are also possible
            //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<TodoItem> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(TodoItem item)
        {
            await Init();
            if (item.ID != 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(TodoItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
            */

    }
}
