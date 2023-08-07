using MediatR;
using PureStore.Application.Interfaces.Services;
using PureStore.Domain.Common;
using PureStore.Domain.Entities;

namespace PureStore.Application.Features.Uploads.Queries
{
    public class DownloadCommand : IRequest<Response<UploadedApplication>>
    {
        public string ApplicationId { get; private set; }

        public DownloadCommand(string applicationId)
        {
            ApplicationId = applicationId;
        }
    }

    public class DownloadCommandHandler : IRequestHandler<DownloadCommand, Response<UploadedApplication>>
    {
        private readonly IUploadApplicationService _uploadApplicationService;

        public DownloadCommandHandler(IUploadApplicationService uploadApplicationService)
        {
            _uploadApplicationService = uploadApplicationService;
        }

        public async Task<Response<UploadedApplication>> Handle(DownloadCommand request, CancellationToken cancellationToken)
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
