using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PureStore.Domain.Common;

namespace PureStore.Persistence.Contexts;

public class MongoContext : IMongoContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoClient _client;
    private readonly IClientSessionHandle _sessionHandle;

    public MongoContext(IOptions<DBSetting> settings)
    {
        _client = new MongoClient(settings.Value.ConnectionString);
        _database = _client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<TEntity> GetCollection<TEntity>(string name)
        => _database.GetCollection<TEntity>(name);
}
