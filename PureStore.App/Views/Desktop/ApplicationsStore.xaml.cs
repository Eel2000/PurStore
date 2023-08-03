using PureStore.App.ViewModels.Desktop;
using CommunityToolkit.Mvvm.Messaging;
using PureStore.App.Messages;

#if WINDOWS
using Microsoft.UI.Input;
using PureStore.App.Extensions;
#endif

namespace PureStore.App.Views.Desktop;

public partial class ApplicationsStore : ContentPage
{
    public ApplicationsStore(ApplicationStoreViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
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

    private void AppsList_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new LoadMoreAppsMessage("LoadMore"));
    }
}