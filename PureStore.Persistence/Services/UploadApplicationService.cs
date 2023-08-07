using PureStore.Application.DTOs.UploadApps;
using PureStore.Application.Interfaces.Repositories;
using PureStore.Application.Interfaces.Services;
using PureStore.Domain.Entities;

namespace PureStore.Persistence.Services
{
    public class UploadApplicationService : IUploadApplicationService
    {
        private readonly IUploadedApplicationRepositoryAsync _uploadedApplicationRepositoryAsync;
        private readonly IDownloadingRepository _downloadingRepository;

        public UploadApplicationService(IUploadedApplicationRepositoryAsync uploadedApplicationRepositoryAsync, IDownloadingRepository downloadingRepository)
        {
            _uploadedApplicationRepositoryAsync = uploadedApplicationRepositoryAsync;
            _downloadingRepository = downloadingRepository;
        }

        public async ValueTask<IEnumerable<UploadedApplication>> GetAllAsync()
            => await _uploadedApplicationRepositoryAsync.ToListAsync(x => x.IsActive == true);

        public async ValueTask<UploadedApplication> RemoveAsync(string id)
        {
            var data = await _uploadedApplicationRepositoryAsync.FirstOrDefaultAsync(x => x.Id == id);
            if (data is null)
                throw new KeyNotFoundException("The application was not found for this operation");

            await _uploadedApplicationRepositoryAsync.DeleteAsync(x => x.Id == id && x.IsActive);
            await _downloadingRepository.DeleteAsync(x => x.applicationId == id);

            return data;

        }

        public async ValueTask<UploadedApplication> DownloadAppAsync(string applicationId)
        {
            var data = await _uploadedApplicationRepositoryAsync.FirstOrDefaultAsync(x => x.Id == applicationId);
            if (data is null)
                throw new KeyNotFoundException("The application was not found for download");

            var download = await _downloadingRepository.FirstOrDefaultAsync(x => x.applicationId == data.Id);
            if (download is null)
            {
                Downloading newStats = new() { applicationId = data.Id, DownloadTime = 1 };

                await _downloadingRepository.AddAsync(newStats);
            }

            download.DownloadTime++;
            await _downloadingRepository.UpdateAsync(download);

            return data;
        }

        public async ValueTask UploadNewAsync(UploadApp app)
        {
            UploadedApplication application = app;
            application.IsActive = true;

            var up = await _uploadedApplicationRepositoryAsync.AddAsync(application);

            Downloading newStats = new() { applicationId = up.Id, DownloadTime = 0 };

            await _downloadingRepository.AddAsync(newStats);
        }
    }
}
