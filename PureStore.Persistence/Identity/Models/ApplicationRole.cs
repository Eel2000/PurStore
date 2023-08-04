using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace PureStore.Persistence.Identity.Models;

[CollectionName("Roles")]
public class ApplicationRole : MongoIdentityRole<Guid>
{
}
