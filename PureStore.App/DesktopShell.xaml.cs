using PureStore.App.Views.Desktop.DetailsPages;

namespace PureStore.App;

public partial class DesktopShell : Shell
{
	public DesktopShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("viewApp", typeof(ViewAppPage));
		Routing.RegisterRoute("home", typeof(DesktopShell));
	}
}