using PureStore.Application.DTOs.UploadApps;
using PureStore.Domain.Entities;

namespace PureStore.Application.Interfaces.Services;

public interface IUploadApplicationService
{
    ValueTask UploadNewAsync(UploadApp app);
    ValueTask<UploadedApplication> RemoveAsync(string id);
    ValueTask<IEnumerable<UploadedApplication>> GetAllAsync();
    ValueTask<UploadedApplication> DownloadAppAsync(string applicationId);
}