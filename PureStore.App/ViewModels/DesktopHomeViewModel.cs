using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PureStore.App.Models;
using PureStore.App.ViewModels.Desktop;
using PureStore.App.Views.Desktop.DetailsPages;
using System.Collections.ObjectModel;

namespace PureStore.App.ViewModels;

public partial class DesktopHomeViewModel : ObservableObject
{
    private ViewAppPageViewModel _pageViewModel;

    [ObservableProperty]
    private ObservableCollection<ItemApp> _apps;

    public DesktopHomeViewModel(ViewAppPageViewModel pageViewModel)
    {
        LoadMostDownloaded();
        _pageViewModel = pageViewModel;
    }

    private void LoadMostDownloaded()
    {
        Apps = new();

        for (int i = 0; i < 10; i++)
        {
            var newApp = new ItemApp()
            {
                Id = Guid.NewGuid(),
                Title = "Calculator mp",
                Author = "Guidance show",
                Size = 5.6f,
                AverageOldYear = 6,
                Description = "Simple calculator to help people.",
                PublicationDate = DateTime.Now,
                Rating = 3.4,
                ImageUrl = "https://wallpaper.dog/large/164795.jpg"
            };

            Apps.Add(newApp);
        }
    }

    [RelayCommand]
    Task ShowDetails(object arg)
    {
        try
        {
            //Shell.Current.GoToAsync("//viewApp");
            Shell.Current.Navigation.PushModalAsync(new ViewAppPage(arg as ItemApp, _pageViewModel), true);
            return Task.CompletedTask;
        }
        catch (Exception)
        {
            return Task.CompletedTask;
        }
    }
}
