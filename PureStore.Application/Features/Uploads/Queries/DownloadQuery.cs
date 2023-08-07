using MediatR;
using PureStore.Application.Interfaces.Services;
using PureStore.Domain.Common;
using PureStore.Domain.Entities;

namespace PureStore.Application.Features.Uploads.Queries
{
    public class DownloadQuery : IRequest<Response<UploadedApplication>>
    {
        public string ApplicationId { get; private set; }

        public DownloadQuery(string applicationId)
        {
            ApplicationId = applicationId;
        }
    }

    public class DownloadQueryHandler : IRequestHandler<DownloadQuery, Response<UploadedApplication>>
    {
        private readonly IUploadApplicationService _uploadApplicationService;

        public DownloadQueryHandler(IUploadApplicationService uploadApplicationService)
        {
            _uploadApplicationService = uploadApplicationService;
        }

        public async Task<Response<UploadedApplication>> Handle(DownloadQuery request, CancellationToken cancellationToken)
        {
            var response = await _uploadApplicationService.DownloadAppAsync(request.ApplicationId);
            if (response == null)
            {
                return new Response<UploadedApplication>("failed to upload application");
            }


            return new Response<UploadedApplication>(response);
        }
    }
}
