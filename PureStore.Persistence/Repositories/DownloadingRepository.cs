using PureStore.Application.Interfaces.Repositories;
using PureStore.Domain.Entities;
using PureStore.Persistence.Contexts;

namespace PureStore.Persistence.Repositories
{
    public class DownloadingRepository : GenericRepositoryAsync<Downloading>, IDownloadingRepository
    {
        public DownloadingRepository(IMongoContext mongoContext) : base(mongoContext)
        {
        }
    }
}
