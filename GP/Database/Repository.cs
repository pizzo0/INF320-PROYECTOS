using SQLite;

namespace GP.Database;

public class Repository<T> where T : new()
{
    protected readonly SQLiteConnection _conn;

    public Repository(SQLiteConnection conn)
    {
        _conn = conn;
        _conn.CreateTable<T>();
    }
    
    public List<T> Get()
    {
        return _conn.Table<T>().ToList();
    }
    public T? GetById(int id)
    {
        return _conn.Find<T>(id);
    }
    public int Insert(T entity)
    {
        return _conn.Insert(entity);
    }
    public int Update(T entity)
    {
        return _conn.Update(entity);
    }
    public int Delete(T entity)
    {
        return _conn.Delete(entity);
    }
}