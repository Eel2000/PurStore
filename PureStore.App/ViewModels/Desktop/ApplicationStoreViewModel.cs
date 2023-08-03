using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PureStore.App.Messages;
using PureStore.App.Models;
using PureStore.App.Services.Interfaces;
using PureStore.App.Views.Desktop.DetailsPages;
using System.Collections.ObjectModel;

namespace PureStore.App.ViewModels.Desktop;

public partial class ApplicationStoreViewModel : ObservableObject
{
    private readonly ViewAppPageViewModel _viewAppPageViewModel;

    private readonly IStoreService _storeService;


    public ApplicationStoreViewModel(ViewAppPageViewModel viewAppPageViewModel, IStoreService storeService)
    {
        _viewAppPageViewModel = viewAppPageViewModel;
        _storeService = storeService;

        LoadAppsDownloaded();

        WeakReferenceMessenger.Default.Register<LoadMoreAppsMessage>(this, (_, data) =>
        {
            LoadMore();
        });
    }

    [ObservableProperty]
    private ObservableCollection<ItemApp> _apps;


    private async void LoadAppsDownloaded()
    {
        Apps = new(await _storeService.GetAsync());
    }

    [RelayCommand]
    Task ShowDetails(object arg)
    {
        try
        {
            Shell.Current.Navigation.PushModalAsync(new ViewAppPage(arg as ItemApp, _viewAppPageViewModel), true);
            return Task.CompletedTask;
        }
        catch (Exception)
        {
            return Task.CompletedTask;
        }
    }

    [RelayCommand]
    async Task LoadMore()
    {
        try
        {
            var data = await _storeService.GetItemApps(12);
            foreach (var item in data)
            {
                Apps.Add(item);
            }

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Pure Store", "Failed to load data " + ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task Search(object? arg)
    {
        try
        {
            if (arg is not null)
            {
                string str = arg.ToString();
                var requested = await _storeService.GetAsync(str);
                Apps = new(requested);
            }
            else
            {
                await Shell.Current.DisplayAlert("Pure Store", "App not found", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Pure Store", "App not found\n " + ex.Message, "OK");
        }
    }

}
