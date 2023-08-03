using PureStore.App.Models;

namespace PureStore.App.Services.Interfaces
{
    public interface IUploadingService
    {
        ValueTask<IEnumerable<Upload>> GetUploadedApplications();
    }
}
