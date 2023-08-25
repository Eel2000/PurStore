using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PureStore.App.Models;
using PureStore.Domain.Entities;
using System.Collections.ObjectModel;

namespace PureStore.App.ViewModels.Desktop;

public partial class ViewAppPageViewModel : ObservableObject
{

    [ObservableProperty]
    public ObservableCollection<Feedback> _feedBacks;

    //[ObservableProperty]
    //public ObservableCollection<Feedback> feedbacks;


    public ViewAppPageViewModel()
    {
        //LoadDataAsync();
    }

    //private void LoadDataAsync()
    //{
    //    var rnd = new Random(1);
    //    FeedBacks = new();
    //    for (int i = 0; i < 10; i++)
    //    {
    //        FeedBack feed = new()
    //        {
    //            Id = Guid.NewGuid().ToString(),
    //            Content = "Awesome app, I've been enjoying it",
    //            Username = "User" + i,
    //            Rating = rnd.Next(1, 5),
    //        };

    //        FeedBacks.Add(feed);
    //    }
    //}

    [RelayCommand]
    Task Back()
    {
        //Shell.Current.GoToAsync("//home");
        Shell.Current.Navigation.PopToRootAsync();
        return Task.CompletedTask;
    }

    [RelayCommand]
    Task Download(object app)
    {
        try
        {
            Shell.Current.DisplaySnackbar("Download started");
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            return Task.CompletedTask;
        }
    }
}
