using MediatR;
using PureStore.Application.DTOs.Feedbacks;
using PureStore.Application.Interfaces.Services;
using PureStore.Domain.Common;

namespace PureStore.Application.Features.Uploads.Commands
{
    public class RateAppCommand : IRequest<Response<string>>
    {
        public FeedBackDTO FeedBack { get; private set; }

        public RateAppCommand(FeedBackDTO feedBack)
        {
            FeedBack = feedBack;
        }
    }

    public class RateAppCommandHandler : IRequestHandler<RateAppCommand, Response<string>>
    {
        private readonly IUploadApplicationService _uploadApplicationService;

        public RateAppCommandHandler(IUploadApplicationService uploadApplicationService)
        {
            _uploadApplicationService = uploadApplicationService;
        }

        public async Task<Response<string>> Handle(RateAppCommand request, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrEmpty(request.FeedBack.ApplicationId, "ApplicationId");
            if (request.FeedBack.Rating <= 0)
                throw new ArithmeticException("Rating can't ben less than 0");

            await _uploadApplicationService.PublishFeedBack(request.FeedBack);

            return new Response<string>(true, "Thank you for feedback");
        }
    }
}
