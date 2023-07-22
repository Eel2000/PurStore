#if WINDOWS
using Microsoft.UI.Input;
using PureStore.App.Extensions;
#endif

namespace PureStore.App.Views.Desktop;

public partial class DesktopHome : ContentPage
{
	public DesktopHome()
	{
		InitializeComponent();
	}

    private void PointerGestureRecognizer_PointerEntered(object sender, Microsoft.Maui.Controls.PointerEventArgs e)
    {
#if WINDOWS
        if (sender is Border frame && frame.Handler.PlatformView is Microsoft.UI.Xaml.Controls.Border nativeFrame)
        {
            nativeFrame.ChangeCursor(InputSystemCursor.Create(InputSystemCursorShape.Hand));
        }
#endif
    }

    private void PointerGestureRecognizer_PointerExited(object sender, Microsoft.Maui.Controls.PointerEventArgs e)
    {
#if WINDOWS
        if (sender is Border frame && frame.Handler.PlatformView is Microsoft.UI.Xaml.Controls.Border nativeFrame)
        {
            nativeFrame.ChangeCursor(InputSystemCursor.Create(InputSystemCursorShape.Arrow));
        }
#endif
    }
}