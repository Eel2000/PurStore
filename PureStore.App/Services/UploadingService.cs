using Newtonsoft.Json;
using PureStore.App.Extensions;
using PureStore.App.Models;
using PureStore.App.Services.Interfaces;
using PureStore.Domain.Common;
using PureStore.Domain.Entities;

namespace PureStore.App.Services
{
    public class UploadingService : IUploadingService
    {
        private List<Upload> _uploads;

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

        private readonly HttpClient _http;

        public UploadingService(HttpClient http = null)
        {
            InitializData();
            _http = http;
        }

        private void InitializData()
        {
            _uploads = new();
            var rnd = new Random();
            for (int i = 0; i < 2; i++)
            {
                var newApp = new Upload()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = Path.GetRandomFileName(),
                    Author = "Guidance show",
                    Size = 5.6f,
                    AverageOldYear = 6,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." +
                    " It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Uploaded = DateTime.Now,
                    ImageUrl = posters[rnd.Next(0, (posters.Length - 1))],
                    Completed = rnd.NextBool()
                };

                _uploads.Add(newApp);
            }
        }

        public async ValueTask<IEnumerable<Upload>> GetUploadedApplications() => _uploads;

        public async ValueTask<Response<IEnumerable<Upload>>> GetMostDownloadedAsync()
        {
            var req = await _http.GetAsync("https://localhost:44313/api/Uploading/most-downloaded");
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
    }
}
