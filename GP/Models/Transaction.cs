using SQLite;

namespace GP.Models;

public class Transaction
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Amount { get; set; } = 0;
    public DateTime Date { get; set; } = DateTime.Today;
    public bool IsIncome { get; set; } = true;
}