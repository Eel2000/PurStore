using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PureStore.App.Messages
{
    public class LoadMoreAppsMessage : ValueChangedMessage<string>
    {
        public LoadMoreAppsMessage(string value) : base(value)
        {
        }
    }
}
