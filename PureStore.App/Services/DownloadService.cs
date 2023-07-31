using PureStore.App.Extensions;
using PureStore.App.Models;
using PureStore.App.Services.Interfaces;

namespace PureStore.App.Services;

public class DownloadService : IDownloadService
{
    private List<ItemApp> _appsInstalledOrDownloaded;

    private readonly string[] posters = new[]
    {
        "https://wallpaper.dog/large/10852364.jpg",
        "https://wallpaper.dog/large/10852421.jpg",
        "https://wallpaper.dog/large/10716693.jpg",
        "https://wallpaper.dog/large/10852412.jpg",
        "https://wallpaper.dog/large/818766.jpg",
        "https://wallpaper.dog/large/10158.jpg",
        "https://wallpaper.dog/large/10852334.jpg",
        "https://wallpaper.dog/large/10852347.jpg",
        "https://wallpaper.dog/large/10852356.jpg",
        "https://wallpaper.dog/large/10698945.jpg",
    };


    public DownloadService()
    {
        InitializData();
    }


    private void InitializData()
    {
        _appsInstalledOrDownloaded = new();
        var rnd = new Random();
        for (int i = 0; i < 7; i++)
        {
            var newApp = new ItemApp()
            {
                Id = Guid.NewGuid(),
                Title = Path.GetRandomFileName(),
                Author = "Guidance show",
                Size = 5.6f,
                AverageOldYear = 6,
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." +
                " It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                PublicationDate = DateTime.Now,
                Rating = rnd.Next(2, 5),
                ImageUrl = posters[rnd.Next(0, (posters.Length - 1))],
                IsInstalled = rnd.NextBool(),
                isDownloading = rnd.NextBool(20),//return random bool value with 20% probality
                LocalPath = Path.Combine(Environment.SpecialFolder.CommonProgramFilesX86.ToString(), "App #" + i)
            };

            _appsInstalledOrDownloaded.Add(newApp);
        }
    }

    public async ValueTask<IEnumerable<ItemApp>> GetDownloadedOrInstalledAsync() => _appsInstalledOrDownloaded;
}
