using PureStore.Application.DTOs.Identity;
using PureStore.Domain.Common;

namespace PureStore.Application.Interfaces.Identity
{
    public interface IAuthenticationService
    {
        ValueTask<Response<object>> AuthenticateAsync(Auth auth);
        ValueTask<Response<object>> RegisterAsync(RegisterDTO user);
        ValueTask<Response<string>> GetApiKeyAsync(Auth auth);
    }
}
