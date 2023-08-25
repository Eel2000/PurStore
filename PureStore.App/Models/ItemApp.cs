using PureStore.Domain.Entities;
using System.Collections.ObjectModel;

namespace PureStore.App.Models;

public class ItemApp
{
    public string Id { get; set; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public string DownloadUrl { get; set; } = string.Empty;
    public float Size { get; set; }
    public DateTime PublicationDate { get; set; }
    public double Rating { get; set; }
    public int AverageOldYear { get; set; }
    public string LocalPath { get; set; }
    public bool IsInstalled { get; set; }
    public bool isDownloading { get; set; }
    public Collection<Feedback> Feedbacks { get; set; }

    public static implicit operator ItemApp(UploadedApplication application)
    {
        return new()
        {
            Id = application.Id,
            ImageUrl = application.ImageUrl,
            Title = application.Title,
            Description = application.Description,
            Author = application.Author,
            DownloadUrl = application.AppUrl,
            AverageOldYear = application.MinYearOld,
            Size = application.Size,
            Rating = application.Feedbacks.Any() ? application.Feedbacks.Average(x => x.Rating) : 0,
            Feedbacks = application.Feedbacks ?? new Collection<Feedback>()
        };
    }
}
