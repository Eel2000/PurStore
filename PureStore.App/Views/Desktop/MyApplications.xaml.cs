using PureStore.App.ViewModels.Desktop;

namespace PureStore.App.Views.Desktop;

public partial class MyApplications : ContentPage
{
    public MyApplications(MyApplicationPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}