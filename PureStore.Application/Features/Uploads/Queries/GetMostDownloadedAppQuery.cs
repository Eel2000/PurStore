using MediatR;
using PureStore.Application.Interfaces.Services;
using PureStore.Domain.Common;
using PureStore.Domain.Entities;

namespace PureStore.Application.Features.Uploads.Queries
{
    public class GetMostDownloadedAppQuery : IRequest<Response<IEnumerable<UploadedApplication>>>
    {
    }

    public class GetMostDownloadedAppQueryHandler : IRequestHandler<GetMostDownloadedAppQuery, Response<IEnumerable<UploadedApplication>>>
    {
        private readonly IUploadApplicationService _uploadApplicationService;

        public GetMostDownloadedAppQueryHandler(IUploadApplicationService uploadApplicationService)
        {
            _uploadApplicationService = uploadApplicationService;
        }

        public async Task<Response<IEnumerable<UploadedApplication>>> Handle(GetMostDownloadedAppQuery request, CancellationToken cancellationToken)
        {
            var data = await _uploadApplicationService.GetTheTwentyMostDownloadedAppAsync();
            return new Response<IEnumerable<UploadedApplication>>(data);
        }
    }
}
