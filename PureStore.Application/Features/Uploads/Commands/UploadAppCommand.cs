using MediatR;
using PureStore.Application.DTOs.UploadApps;
using PureStore.Application.Interfaces.Services;
using PureStore.Domain.Common;

namespace PureStore.Application.Features.Uploads.Commands
{
    public class UploadAppCommand : IRequest<Response<string>>
    {
        public UploadApp App { get; private set; }

        public UploadAppCommand(UploadApp app)
        {
            App = app;
        }
    }

    public class UploadAppCommandHandler : IRequestHandler<UploadAppCommand, Response<string>>
    {
        private readonly IUploadApplicationService _uploadApplicationService;

        public UploadAppCommandHandler(IUploadApplicationService uploadApplicationService)
        {
            _uploadApplicationService = uploadApplicationService;
        }

        public async Task<Response<string>> Handle(UploadAppCommand request, CancellationToken cancellationToken)
        {
            await _uploadApplicationService.UploadNewAsync(request.App);

            return new Response<string>(default, "Upload completed!");
        }
    }
}
