using GP.Models;
using SQLite;

namespace GP.Database;

public class UserRepository(SQLiteConnection conn) : Repository<User>(conn)
{
    public User? GetUser()
    {
        return _conn.Table<User>().FirstOrDefault();
    }

    public int Save(User user)
    {
        var curr = GetUser();
        if (curr is null)
        {
            return Insert(user);
        }

        user.Id = curr.Id;
        return Update(user);
    }
}
