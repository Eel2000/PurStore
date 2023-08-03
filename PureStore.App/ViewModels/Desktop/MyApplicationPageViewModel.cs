using CommunityToolkit.Mvvm.ComponentModel;
using PureStore.App.Models;
using PureStore.App.Services.Interfaces;
using System.Collections.ObjectModel;

namespace PureStore.App.ViewModels.Desktop;

public partial class MyApplicationPageViewModel : ObservableObject
{
    private readonly IUploadingService _uploadingService;

    public MyApplicationPageViewModel(IUploadingService uploadingService)
    {
        _uploadingService = uploadingService;

        LoadData();
    }

    [ObservableProperty]
    private ObservableCollection<Upload> _apps;

    private async void LoadData()
    {
        try
        {
            Apps = new(await _uploadingService.GetUploadedApplications());
        }
        catch (Exception)
        {
            //TODO: show the exception
        }
    }
}
