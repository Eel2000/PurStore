using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using PureStore.Application.Interfaces;
using PureStore.Persistence.Contexts;
using ServiceStack;
using System.Linq.Expressions;

namespace PureStore.Persistence.Repositories;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
{
    protected readonly IMongoContext _mongoContext;
    protected readonly IMongoCollection<T> _collection;

    public GenericRepositoryAsync(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;

        _collection = _mongoContext.GetCollection<T>(typeof(T).Name);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        var id = entity.GetObjectId();
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> expression)
        => await _collection.DeleteOneAsync(expression);

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        => await _collection.Find(expression).FirstOrDefaultAsync();

    public async Task<IReadOnlyList<T>> GetAllAsync()
        => _collection.AsQueryable().ToList();

    public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
    {
        return await _collection
             .AsQueryable()
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();
    }

    public async Task<IEnumerable<T>> ToListAsync(Expression<Func<T, bool>> expression)
    {
        return await _collection.Find(expression).ToListAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        object id = entity.GetObjectId();

        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), entity);
    }

    public async Task UpdateAsync(Expression<Func<T, bool>> expression, T update)
        => await _collection.ReplaceOneAsync(expression, update);
}
