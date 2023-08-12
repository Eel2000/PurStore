using PureStore.Domain.Entities;
using System.Collections.ObjectModel;

namespace PureStore.App.Models;

public class Upload
{
    public string Id { get; set; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public string AuthorUrl { get; set; }
    public string AppUrl { get; set; }
    public DateTime Uploaded { get; set; } = DateTime.Now;
    public bool Completed { get; set; }
    public int AverageOldYear { get; set; }
    public float Size { get; set; }
    public double Rating { get; set; }
    public Collection<Feedback> FeedBacks { get; set; }

    public static implicit operator Upload(UploadedApplication application)
    {
        return new()
        {
            Id = application.Id,
            ImageUrl = application.ImageUrl,
            Title = application.Title,
            Description = application.Description,
            Author = application.Author,
            AuthorUrl = application.AuthorUrl,
            AppUrl = application.AppUrl,
            Uploaded = application.Uploaded,
            Completed = application.Completed,
            AverageOldYear = application.MinYearOld,
            Size = application.Size,
            Rating = application.Feedbacks.Any() ? application.Feedbacks.Average(x => x.Rating) : 0,
            FeedBacks = application.Feedbacks ?? new Collection<Feedback>()
        };
    }
}
