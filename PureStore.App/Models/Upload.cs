namespace PureStore.App.Models;

public class Upload
{
    public Guid Id { get; set; }
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
}
