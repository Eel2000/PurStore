using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using PureStore.Application.DTOs.Identity;

namespace PureStore.Persistence.Identity.Models;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{

    public static explicit operator UserDTO(ApplicationUser user)
        => new UserDTO(user.UserName, user.Email);

    public static implicit operator ApplicationUser(RegisterDTO auth)
    {
        return new ApplicationUser
        {
            UserName = auth.Username,
            Email = auth.Email,
        };
    }
}
