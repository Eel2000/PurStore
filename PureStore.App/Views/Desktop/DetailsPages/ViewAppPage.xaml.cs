using PureStore.App.Models;
using PureStore.App.ViewModels.Desktop;

namespace PureStore.App.Views.Desktop.DetailsPages;

public partial class ViewAppPage : ContentPage
{
    private ItemApp app;
    private Upload _app;

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

        if (Uri.TryCreate(item.ImageUrl, UriKind.Absolute, out var uriResult) && uriResult.Scheme == Uri.UriSchemeHttps)
            AppBanner.Source = ImageSource.FromUri(uriResult);

        LblTitle.Text = item.Title;
        LblpubDate.Text = "Published: " + item.PublicationDate.ToString("d");
        lblDescription.Text = item.Description;

        rating.Value = item.Rating;
    }

    public ViewAppPage(Upload item, ViewAppPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

        _app = item;

        if (Uri.TryCreate(item.ImageUrl, UriKind.Absolute, out var uriResult) && uriResult.Scheme == Uri.UriSchemeHttps)
            AppBanner.Source = ImageSource.FromUri(uriResult);

        LblTitle.Text = item.Title;
        LblpubDate.Text = "Published: " + item.Uploaded.ToString("d");
        lblDescription.Text = item.Description;

        rating.Value = item.Rating;
    }
}