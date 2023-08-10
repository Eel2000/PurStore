using PureStore.Domain.Common;
using System.Collections.ObjectModel;

namespace PureStore.Domain.Entities;

public class UploadedApplication : BaseEntity
{
    public UploadedApplication()
    {
        Feedbacks = new();
    }

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

    public Collection<Feedback> Feedbacks { get; set; }
}
