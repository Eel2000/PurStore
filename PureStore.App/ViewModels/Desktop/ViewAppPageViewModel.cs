using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PureStore.App.Models;
using System.Collections.ObjectModel;

namespace PureStore.App.ViewModels.Desktop
{
    public partial class ViewAppPageViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<FeedBack> _feedBacks;


        public ViewAppPageViewModel()
        {
            LoadDataAsync();
        }

        private void LoadDataAsync()
        {
            var rnd = new Random(1);
            FeedBacks = new();
            for (int i = 0; i < 10; i++)
            {
                FeedBack feed = new()
                {
                    Id = Guid.NewGuid(),
                    Content = "Awesome app, I've been enjoying it",
                    Username = "User" + i,
                    Rating = rnd.Next(1, 5),
                };

                FeedBacks.Add(feed);
            }
        }

        [RelayCommand]
        Task Back()
        {
            //Shell.Current.GoToAsync("//home");
            Shell.Current.Navigation.PopToRootAsync();
            return Task.CompletedTask;
        }
    }
}
