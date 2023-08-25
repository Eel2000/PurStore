using Newtonsoft.Json;
using PureStore.App.Models;
using PureStore.App.Services.Interfaces;
using PureStore.Domain.Common;
using PureStore.Domain.Entities;

namespace PureStore.App.Services;

public class StoreService : IStoreService
{

    private List<ItemApp> itemApps;
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

    private readonly HttpClient _httpClient;

    public StoreService(HttpClient httpClient)
    {
        InitializData();
        _httpClient = httpClient;
    }

    public async ValueTask<IEnumerable<ItemApp>> GetAsync(string key)
    {
        var data = itemApps.Where(app => app.Title.Contains(key, StringComparison.CurrentCultureIgnoreCase));
        return data;
    }

    public async ValueTask<IEnumerable<ItemApp>> GetAsync()
    {
        return itemApps;
    }

    public async ValueTask<Response<IEnumerable<Upload>>> GetUploads()
    {
        var req = await _httpClient.GetAsync("https://localhost:44313/api/Uploading/applications");
        if (req.IsSuccessStatusCode)
        {
            var response = await req.Content.ReadAsStringAsync();
            var raw = JsonConvert.DeserializeObject<Response<IEnumerable<UploadedApplication>>>(response);

            var data = new List<Upload>();
            foreach (var item in raw.Data)
            {
                data.Add(item);
            }

            return new Response<IEnumerable<Upload>>(raw.Succeeded.Value, data, raw.Message);
        }

        return new Response<IEnumerable<Upload>>();
    }

    public async ValueTask<IEnumerable<ItemApp>> GetItemApps(int number)
        => itemApps.Skip(number).Take(number).ToList();

    private void InitializData()
    {
        itemApps = new();
        var rnd = new Random();
        for (int i = 0; i < 150; i++)
        {
            var newApp = new ItemApp()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "App #" + i,
                Author = "Guidance show",
                Size = 5.6f,
                AverageOldYear = 6,
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." +
                " It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                PublicationDate = DateTime.Now,
                Rating = rnd.Next(2, 5),
                ImageUrl = posters[rnd.Next(0, (posters.Length - 1))]
            };

            itemApps.Add(newApp);
        }
    }
}
