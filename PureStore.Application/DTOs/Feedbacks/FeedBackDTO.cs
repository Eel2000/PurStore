using PureStore.Domain.Entities;

namespace PureStore.Domain.DTOs.Feedbacks
{
    public record FeedBackDTO(string Content, double Rating, string Username)
    {
        public static explicit operator Feedback(FeedBackDTO feedback)
        {
            return new Feedback
            {
                Content = feedback.Content,
                Rating = feedback.Rating,
                Username = feedback.Username
            };
        }
    }
}
