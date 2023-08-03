using PureStore.App.Models;

namespace PureStore.App.Templates;

public class UploadAppTemplateSelector : DataTemplateSelector
{
    public DataTemplate UploadingTemplate { get; set; }
    public DataTemplate UploadedTemplate { get; set; }
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    => ((Upload)item).Completed ? UploadedTemplate : UploadingTemplate;
}
