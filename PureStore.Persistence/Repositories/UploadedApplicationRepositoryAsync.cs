using PureStore.Application.Interfaces.Repositories;
using PureStore.Domain.Entities;
using PureStore.Persistence.Contexts;

namespace PureStore.Persistence.Repositories;

public class UploadedApplicationRepositoryAsync : GenericRepositoryAsync<UploadedApplication>, IUploadedApplicationRepositoryAsync
{
    public UploadedApplicationRepositoryAsync(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}
