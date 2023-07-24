namespace PureStore.App.Models
{
    public class FeedBack
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public double Rating { get; set; }
        public DateOnly PublishedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
