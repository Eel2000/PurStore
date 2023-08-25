using PureStore.App.Models;
using PureStore.Domain.Common;

namespace PureStore.App.Services.Interfaces
{
    public interface IUploadingService
    {
        ValueTask<Response<IEnumerable<Upload>>> GetMostDownloadedAsync();
        ValueTask<IEnumerable<Upload>> GetUploadedApplications();
    }
}
