namespace PureStore.App.Views.Desktop;

public partial class User : ContentPage
{
    public User()
    {
        InitializeComponent();
    }

    private void btnChangeTheme_Clicked(object sender, EventArgs e)
    {
        App.Current.UserAppTheme = AppTheme.Dark;
    }
}