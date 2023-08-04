using PureStore.Domain.Common;

namespace PureStore.Domain.Entities;

public class UploadedApplication : BaseEntity
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
}
