using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PureStore.App.ViewModels.Desktop
{
    public partial class ViewAppPageViewModel : ObservableObject
    {


        [RelayCommand]
        Task Back()
        {
            Shell.Current.GoToAsync("//home");
            return Task.CompletedTask;
        }
    }
}
