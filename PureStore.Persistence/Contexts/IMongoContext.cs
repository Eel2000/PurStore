using MongoDB.Driver;

namespace PureStore.Persistence.Contexts
{
    public interface IMongoContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name);
    }
}
