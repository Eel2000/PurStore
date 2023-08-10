using PureStore.Domain.Entities;

namespace PureStore.Domain.DTOs.Feedbacks
{
    public record FeedBackDTO(string Content, double Rating, string Username);
}
