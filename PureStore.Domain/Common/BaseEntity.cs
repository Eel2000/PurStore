using MongoDB.Bson.Serialization.Attributes;

namespace PureStore.Domain.Common;

#nullable disable
public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    public bool IsActive { get; set; }
}
