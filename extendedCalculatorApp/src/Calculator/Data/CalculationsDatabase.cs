using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Calculator.Models;

namespace Calculator.Data;
public class CalculationsDatabase
{
    SQLiteAsyncConnection Database;
    public CalculationsDatabase()
    {

    }


    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<DBCalculations>();

    }

    public async Task<List<DBCalculations>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<DBCalculations>().ToListAsync();
    }

    /* this isnt needed for the calculations
    public async Task<List<DBCalculations>> GetItemsNotDoneAsync()
    {
        await Init();
        return await Database.Table<DBCalculations>().Where(t => t.Done).ToListAsync();

        // SQL queries are also possible
        //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
    }
    */

    public async Task<DBCalculations> GetItemAsync(int id)
    {
        await Init();
        return await Database.Table<DBCalculations>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(DBCalculations item)
    {
        await Init();
        if (item.ID != 0)
            return await Database.UpdateAsync(item);
        else
            return await Database.InsertAsync(item);
    }

    public async Task<int> DeleteItemAsync(DBCalculations item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }
}