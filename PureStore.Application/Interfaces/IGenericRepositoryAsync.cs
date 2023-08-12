using System.Linq.Expressions;

namespace PureStore.Application.Interfaces;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task UpdateAsync(Expression<Func<T, bool>> expression, T update);
    Task DeleteAsync(T entity);
    Task DeleteAsync(Expression<Func<T, bool>> expression);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> ToListAsync(Expression<Func<T, bool>> expression);
    //Task<IEnumerable<T>> GetOrderedSizedAsync(Expression<Func<T, bool>> expression, int size);
}
