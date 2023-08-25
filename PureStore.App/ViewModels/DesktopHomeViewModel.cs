using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PureStore.App.Models;
using PureStore.App.Services.Interfaces;
using PureStore.App.ViewModels.Desktop;
using PureStore.App.Views.Desktop.DetailsPages;
using PureStore.Domain.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PureStore.App.ViewModels;

public partial class DesktopHomeViewModel : ObservableObject
{
    private ViewAppPageViewModel _pageViewModel;
    private readonly IUploadingService _uploadingService;

    [ObservableProperty]
    private ObservableCollection<Upload> _apps;



    public DesktopHomeViewModel(ViewAppPageViewModel pageViewModel, IUploadingService uploadingService)
    {
        _pageViewModel = pageViewModel;
        _uploadingService = uploadingService;

        Task.Run(async () => await LoadMostDownloaded());
    }

    private async ValueTask LoadMostDownloaded()
    {
        try
        {
            Apps = new();
            var response = await _uploadingService.GetMostDownloadedAsync();
            if (response.Succeeded.Value)
            {
                Apps = new(response.Data);
            }
            else
            {
                await Shell.Current.DisplayAlert("Pure Store", response.Message, "Ok");
            }

        }
        catch (Exception e)
        {
            Debug.Write(e);
            await Shell.Current.DisplayAlert("Pure Store", e.Message, "Ok");
        }
    }

    [RelayCommand]
    Task ShowDetails(object arg)
    {
        try
        {
            //Shell.Current.GoToAsync("//viewApp");
            Shell.Current.Navigation.PushModalAsync(new ViewAppPage(arg as Upload, _pageViewModel), true);
            return Task.CompletedTask;
        }
        catch (Exception)
        {
            return Task.CompletedTask;
        }
    }
}
