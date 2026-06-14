using SQLite;
using GP.Models;

namespace GP.Database;

public class TransactionRepository : Repository<Transaction>
{
    public TransactionRepository(SQLiteConnection conn) : base(conn) { }

    public (int Income, int Expenses, int Balance) GetSummary()
    {
        var all = _conn.Table<Transaction>().ToList();

        int income = all.Where(t => t.IsIncome).Sum(t => t.Amount);
        int expenses = all.Where(t => !t.IsIncome).Sum(t => t.Amount);

        return (income, expenses, income - expenses);
    }
}