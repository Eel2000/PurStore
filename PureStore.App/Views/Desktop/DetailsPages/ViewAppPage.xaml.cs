using PureStore.App.Models;
using PureStore.App.ViewModels.Desktop;

namespace PureStore.App.Views.Desktop.DetailsPages;

public partial class ViewAppPage : ContentPage
{
    private ItemApp app;

    public ViewAppPage()
    {
        InitializeComponent();
    }


    public ViewAppPage(ItemApp item)
    {
        InitializeComponent();

        app = item;
    }

    public ViewAppPage(ItemApp item, ViewAppPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

        app = item;

        AppBanner.Source = ImageSource.FromUri(new Uri(item.ImageUrl));

        LblTitle.Text = item.Title;
        LblpubDate.Text = "Published: " + item.PublicationDate.ToString("d");
        lblDescription.Text = item.Description;

        rating.Value = item.Rating;
    }
}