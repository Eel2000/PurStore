using LiteDB.Async;
using System.Linq.Expressions;

namespace PureStore.App.DataBase;

public class LocalDatabaseService
{
    private readonly LiteDatabaseAsync _liteDatabase;

    public LocalDatabaseService()
    {
        _liteDatabase = new LiteDatabaseAsync("Filename=pureStoreLocal.db;Connection=shared;Password=pur3St0r3");
    }

    public async ValueTask<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression)
    {
        var collection = _liteDatabase.GetCollection<T>();
        return await collection.Query().Where(expression).FirstOrDefaultAsync();
    }

    public async ValueTask<T> FirstAsync<T>()
    {
        var collection = _liteDatabase.GetCollection<T>();
        return await collection.Query().FirstAsync();
    }

    public async ValueTask<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> expression)
    {
        var collection = _liteDatabase.GetCollection<T>();
        var result = await collection.Query().Where(expression).ToListAsync();

        return result;
    }

    public async ValueTask SaveDataAsync<T>(T tEntity)
    {
        var collection = _liteDatabase.GetCollection<T>();
        var upsert = await collection.UpsertAsync(tEntity);
    }

    public async ValueTask<bool> Clear()
    {
        var collectionsNames = await _liteDatabase.GetCollectionNamesAsync();
        foreach (var item in collectionsNames)
        {
            await _liteDatabase.DropCollectionAsync(item);
        }

        return true;
    }
}
