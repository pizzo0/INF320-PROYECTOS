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
    
    public virtual List<T> Get()
    {
        return [.. _conn.Table<T>()];
    }
    public virtual T? GetById(int id)
    {
        return _conn.Find<T>(id);
    }
    public virtual int Insert(T entity)
    {
        return _conn.Insert(entity);
    }
    public virtual int Update(T entity)
    {
        return _conn.Update(entity);
    }
    public virtual int Delete(T entity)
    {
        return _conn.Delete(entity);
    }
}