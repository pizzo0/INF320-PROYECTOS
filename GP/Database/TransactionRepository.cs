using SQLite;
using GP.Models;

namespace GP.Database;

public class TransactionRepository(SQLiteConnection conn) : Repository<Transaction>(conn)
{
    // solo falta hacer el calculateBalance, calculateIncome y calculateExpenses
    
    public override List<Transaction> Get()
    {
        return [.. _conn.Table<Transaction>().OrderByDescending(t => t.Date)];
    }
}