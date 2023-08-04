using PureStore.Application.Interfaces.Repositories;
using PureStore.Domain.Entities;
using PureStore.Persistence.Contexts;

namespace PureStore.Persistence.Repositories;

public class FeedbackRepositoryAsync : GenericRepositoryAsync<Feedback>, IFeedbackRepositoryAsync
{
    public FeedbackRepositoryAsync(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}
