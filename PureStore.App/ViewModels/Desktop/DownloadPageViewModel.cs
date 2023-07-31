using CommunityToolkit.Mvvm.ComponentModel;
using PureStore.App.Models;
using PureStore.App.Services.Interfaces;
using System.Collections.ObjectModel;

namespace PureStore.App.ViewModels.Desktop;

public partial class DownloadPageViewModel : ObservableObject
{
    private readonly IDownloadService _downloadService;

    public DownloadPageViewModel(IDownloadService downloadService)
    {
        _downloadService = downloadService;

        LoadData();
    }

    [ObservableProperty]
    private ObservableCollection<ItemApp> _apps;

    private async void LoadData()
    {
        try
        {
            Apps = new(await _downloadService.GetDownloadedOrInstalledAsync());
        }
        catch (Exception)
        {
            //TODO: show the exception
        }
    }
}
