using PureStore.Application.DTOs.Identity;
using PureStore.Domain.Common;

namespace PureStore.Application.Interfaces.Identity
{
    public interface IAuthenticationService
    {
        ValueTask<AuthResponse> AuthenticateAsync(Auth auth);
        ValueTask<AuthResponse> RegisterAsync(RegisterDTO user);
        ValueTask<Response<string>> GetApiKeyAsync(Auth auth);
    }
}
