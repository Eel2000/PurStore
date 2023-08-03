using PureStore.App.Extensions;
using PureStore.App.Models;
using PureStore.App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public UploadingService()
        {
            InitializData();
        }

        private void InitializData()
        {
            _uploads = new();
            var rnd = new Random();
            for (int i = 0; i < 2; i++)
            {
                var newApp = new Upload()
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
                    Uploaded = DateTime.Now,
                    ImageUrl = posters[rnd.Next(0, (posters.Length - 1))],
                    Completed = rnd.NextBool()
                };

                _uploads.Add(newApp);
            }
        }

        public async ValueTask<IEnumerable<Upload>> GetUploadedApplications() => _uploads;
    }
}
