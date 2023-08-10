using MediatR;
using PureStore.Application.Interfaces.Services;
using PureStore.Domain.Common;
using PureStore.Domain.Entities;

namespace PureStore.Application.Features.Uploads.Queries
{
    public class GetPagedDataQuery : IRequest<Response<IEnumerable<UploadedApplication>>>
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public GetPagedDataQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPagedDataQueryHandler : IRequestHandler<GetPagedDataQuery, Response<IEnumerable<UploadedApplication>>>
    {
        private readonly IUploadApplicationService _uploadApplicationService;

        public GetPagedDataQueryHandler(IUploadApplicationService uploadApplicationService)
        {
            _uploadApplicationService = uploadApplicationService;
        }

        public async Task<Response<IEnumerable<UploadedApplication>>> Handle(GetPagedDataQuery request, CancellationToken cancellationToken)
        {
            var response = await _uploadApplicationService.GetAllAsync(request.PageNumber, request.PageSize);

            return new Response<IEnumerable<UploadedApplication>>(response);
        }
    }
}
