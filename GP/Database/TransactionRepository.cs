using SQLite;
using GP.Models;

namespace GP.Database;

public class TransactionRepository(SQLiteConnection conn) : Repository<Transaction>(conn)
{
    public decimal CalculateBalance()
    {
        var income = CalculateIncome();
        var expenses = CalculateExpenses();
        return income - expenses;
    }

    public decimal CalculateIncome()
    {
        var transactions = Get();
        decimal income = 0;
        foreach (var transaction in transactions)
        {
            if (transaction.IsIncome)
            {
                income += transaction.Amount;
            }
        }
        return income;
    }

    public decimal CalculateExpenses()
    {
        var transactions = Get();
        decimal expenses = 0;
        foreach (var transaction in transactions)
        {
            if (!transaction.IsIncome)
            {
                expenses += transaction.Amount;
            }
        }
        return expenses;
    }

    public override List<Transaction> Get()
    {
        return [.. _conn.Table<Transaction>().OrderByDescending(t => t.Date)];
    }
}
