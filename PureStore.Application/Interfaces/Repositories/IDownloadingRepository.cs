using PureStore.Domain.Entities;

namespace PureStore.Application.Interfaces.Repositories
{
    public interface IDownloadingRepository : IGenericRepositoryAsync<Downloading>
    {
        /// <summary>
        /// Load the most downloaded app base on the specified size.
        /// </summary>
        /// <param name="size">the number of element to retrieve</param>
        /// <returns><see cref="IEnumerable{T}"/> of <seealso cref="Downloading"/></returns>
        ValueTask<IEnumerable<Downloading>> GetTheMostDownloadedAsync(int size = 20);
    }
}
