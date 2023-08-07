using MediatR;
using PureStore.Application.Interfaces.Services;
using PureStore.Domain.Common;
using PureStore.Domain.Entities;

namespace PureStore.Application.Features.Uploads.Commands
{
    public class RemoveAppCommand : IRequest<Response<UploadedApplication>>
    {
        public string ApplicationId { get; private set; }

        public RemoveAppCommand(string applicationId)
        {
            ApplicationId = applicationId;
        }
    }

    public class RemoveAppCommandHandler : IRequestHandler<RemoveAppCommand, Response<UploadedApplication>>
    {
        private readonly IUploadApplicationService _uploadApplicationService;

        public RemoveAppCommandHandler(IUploadApplicationService uploadApplicationService)
        {
            _uploadApplicationService = uploadApplicationService;
        }

        public async Task<Response<UploadedApplication>> Handle(RemoveAppCommand request, CancellationToken cancellationToken)
        {
            var response = await _uploadApplicationService.RemoveAsync(request.ApplicationId);
            if (response == null)
            {
                return new Response<UploadedApplication>("Failed to remove the specified application. App Not Found");
            }

            return new Response<UploadedApplication>(response, "Operation completed successfully. the app was definetly removed");
        }
    }
}
