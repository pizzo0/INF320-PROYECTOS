using SQLite;

namespace GP.Models;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;
}
