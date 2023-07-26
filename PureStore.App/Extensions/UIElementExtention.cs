#if WINDOWS
using Microsoft.UI.Input;
#endif

using System.Reflection;

namespace PureStore.App.Extensions;

public static class UIElementExtention
{
#if WINDOWS
    public static void ChangeCursor(this Microsoft.UI.Xaml.UIElement uiElement, InputCursor cursor)
    {
        Type type = typeof(Microsoft.UI.Xaml.UIElement);
        type.InvokeMember("ProtectedCursor", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, uiElement, new object[] { cursor });
    }
#endif
}
