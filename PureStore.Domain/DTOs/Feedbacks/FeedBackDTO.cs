using PureStore.Domain.Entities;

namespace PureStore.Application.DTOs.Feedbacks
{
    public record FeedBackDTO(string Content, double Rating, string Username, string ApplicationId)
    {
        public static explicit operator Feedback(FeedBackDTO feedback)
        {
            return new Feedback
            {
                Content = feedback.Content,
                Rating = feedback.Rating,
                Username = feedback.Username,
                ApplicationId = feedback.ApplicationId
            };
        }
    }
}
