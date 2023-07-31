using PureStore.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureStore.App.Templates
{
    public class DownloadItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DownloadingTemplate { get; set; }
        public DataTemplate DownloadedTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        => ((ItemApp)item).isDownloading ? DownloadingTemplate : DownloadedTemplate;
    }
}
