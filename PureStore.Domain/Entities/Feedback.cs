using PureStore.Domain.Common;
using PureStore.Domain.DTOs.Feedbacks;

namespace PureStore.Domain.Entities;

public class Feedback : BaseEntity
{
    public required string Content { get; set; }
    public required string Username { get; set; }
    public required double Rating { get; set; }

    public string Initials
    {
        get
        {
            if (string.IsNullOrEmpty(Username))
            {
                return string.Empty;
            }

            return Username.Remove(2, Username.Length - 2).ToUpper();
        }
    }

    public static implicit operator Feedback(FeedBackDTO feed)
    {
        return new Feedback
        {
            Content = feed.Content,
            Username = feed.Username,
            Rating = feed.Rating,
        };
    }
}
