using PureStore.App.ViewModels.Desktop;

namespace PureStore.App.Views.Desktop;

public partial class Downloaded : ContentPage
{
    public Downloaded(DownloadPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}