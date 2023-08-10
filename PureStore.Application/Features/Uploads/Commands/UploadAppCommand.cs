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
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            ArgumentException.ThrowIfNullOrEmpty(request.App.Title, "Title");
            ArgumentException.ThrowIfNullOrEmpty(request.App.Description, "Description");
            ArgumentException.ThrowIfNullOrEmpty(request.App.AppUrl, "AppUrl");

            if (request.App.Title.Equals("string", comparisonType: StringComparison.OrdinalIgnoreCase))
                throw new ArgumentOutOfRangeException("Title", request.App.Title, "Value not allowed");

            if (request.App.Description.Equals("string", comparisonType: StringComparison.OrdinalIgnoreCase))
                throw new ArgumentOutOfRangeException("Description", request.App.Description, "Value not allowed");

            if (request.App.Author.Equals("string", comparisonType: StringComparison.OrdinalIgnoreCase))
                throw new ArgumentOutOfRangeException("Author", request.App.Author, "Value not allowed");

            if (request.App.AppUrl.Equals("string", comparisonType: StringComparison.OrdinalIgnoreCase))
                throw new ArgumentOutOfRangeException("AppUrl", request.App.AppUrl, "Value not allowed");



            await _uploadApplicationService.UploadNewAsync(request.App);

            return new Response<string>(string.Empty, "Upload completed!");
        }
    }
}
