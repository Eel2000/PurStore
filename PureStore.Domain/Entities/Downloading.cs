using PureStore.Domain.Common;

namespace PureStore.Domain.Entities
{
    public class Downloading : BaseEntity
    {
        public required string applicationId { get; set; }
        public required long DownloadTime { get; set; }
    }
}
