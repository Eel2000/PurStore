using PureStore.Domain.Entities;

namespace PureStore.App.Models
{
    public class FeedBack
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public double Rating { get; set; }
        public DateOnly PublishedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);

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

        public static explicit operator FeedBack(Feedback feedback)
        {
            return new FeedBack()
            {
                Id = feedback.Id,
                Content = feedback.Content,
                Username = feedback.Username,
                Rating = feedback.Rating,
            };
        }
    }
}
