using Calculator.ViewModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Models;


namespace Calculator.Data
{
    public class PreviousCalculationsDatabase
    {
        SQLiteAsyncConnection Database;

        public PreviousCalculationsDatabase()
        { }

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

        // Clear the database.
        public async Task<int> DeleteHistoryAsync()
        {
            await Init();
            return await Database.DeleteAllAsync<PreviousCalculation>();
        }

    }
}
