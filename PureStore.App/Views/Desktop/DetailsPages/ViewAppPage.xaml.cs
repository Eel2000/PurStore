using PureStore.App.Models;
using PureStore.App.ViewModels.Desktop;

namespace PureStore.App.Views.Desktop.DetailsPages;

public partial class ViewAppPage : ContentPage
{
    public ViewAppPage()
    {
        InitializeComponent();
    }


    public ViewAppPage(ItemApp item)
    {
        InitializeComponent();
    }

    public ViewAppPage(ItemApp item, ViewAppPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

        AppBanner.Source = ImageSource.FromUri(new Uri(item.ImageUrl));

        LblTitle.Text = item.Title;
        LblpubDate.Text = "Published: " + item.PublicationDate.ToString("d");
        lblDescription.Text = item.Description;

        rating.Value = item.Rating;
    }
}