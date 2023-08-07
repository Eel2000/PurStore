using MediatR;
using PureStore.Application.Interfaces.Services;
using PureStore.Domain.Common;
using PureStore.Domain.Entities;

namespace PureStore.Application.Features.Uploads.Queries
{
    public class GetAllAppQueries : IRequest<Response<IEnumerable<UploadedApplication>>>
    {
    }

    public class GetAllAppQueriesHandler : IRequestHandler<GetAllAppQueries, Response<IEnumerable<UploadedApplication>>>
    {
        private readonly IUploadApplicationService _uploadApplicationService;

        public GetAllAppQueriesHandler(IUploadApplicationService uploadApplicationService)
        {
            _uploadApplicationService = uploadApplicationService;
        }

        public async Task<Response<IEnumerable<UploadedApplication>>> Handle(GetAllAppQueries request, CancellationToken cancellationToken)
        {
            var data = await _uploadApplicationService.GetAllAsync();

            return new Response<IEnumerable<UploadedApplication>>(data);
        }
    }
}
