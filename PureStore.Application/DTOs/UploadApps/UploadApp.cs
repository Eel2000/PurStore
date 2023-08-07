using PureStore.Domain.Entities;

namespace PureStore.Application.DTOs.UploadApps;

public class UploadApp
{
    public string? ImageUrl { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Author { get; set; }
    public string? AuthorUrl { get; set; }
    public required string AppUrl { get; set; }
    public DateTime Uploaded { get; set; } = DateTime.Now;
    public bool Completed { get; set; }
    public int MinYearOld { get; set; }
    public required float Size { get; set; }

    public static implicit operator UploadedApplication(UploadApp app)
    {
        return new()
        {
            ImageUrl = app.ImageUrl,
            Title = app.Title,
            Description = app.Description,
            Author = app.Author,
            AuthorUrl = app.AuthorUrl,
            AppUrl = app.AppUrl,
            Uploaded = app.Uploaded,
            Completed = app.Completed,
            MinYearOld = app.MinYearOld,
            Size = app.Size
        };
    }
}