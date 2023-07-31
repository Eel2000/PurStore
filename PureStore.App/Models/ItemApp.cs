namespace PureStore.App.Models;

public class ItemApp
{
    public Guid Id { get; set; }
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
}
