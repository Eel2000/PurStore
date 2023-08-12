using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using PureStore.Application.Interfaces.Repositories;
using PureStore.Domain.Entities;
using PureStore.Persistence.Contexts;

namespace PureStore.Persistence.Repositories
{
    public class DownloadingRepository : GenericRepositoryAsync<Downloading>, IDownloadingRepository
    {
        private readonly IMongoCollection<Downloading> _downloadingCollection;
        public DownloadingRepository(IMongoContext mongoContext) : base(mongoContext)
        {
            _downloadingCollection = mongoContext.GetCollection<Downloading>(nameof(Downloading));
        }


        /// <summary>
        /// Load the most downloaded app base on the specified size.
        /// </summary>
        /// <param name="size">the number of element to retrieve</param>
        /// <returns><see cref="IEnumerable{T}"/> of <seealso cref="Downloading"/></returns>
        public async ValueTask<IEnumerable<Downloading>> GetTheMostDownloadedAsync(int size = 20)
        {
            return _collection
            .AsQueryable()
            .OrderByDescending(x => x.DownloadTime)
            .Take(size)
            .ToList();
        }
    }
}
