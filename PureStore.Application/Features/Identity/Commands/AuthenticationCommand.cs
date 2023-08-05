using MediatR;
using PureStore.Application.DTOs.Identity;
using PureStore.Application.Interfaces.Identity;
using PureStore.Domain.Common;

namespace PureStore.Application.Features.Identity.Commands;

public class AuthenticationCommand : IRequest<Response<AuthResponse>>
{
    public Auth Auth { get; private set; }

    public AuthenticationCommand(Auth auth)
    {
        Auth = auth;
    }
}

public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, Response<AuthResponse>>
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Response<AuthResponse>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        ArgumentException.ThrowIfNullOrEmpty(request.Auth.Username, "Username");
        ArgumentException.ThrowIfNullOrEmpty(request.Auth.Password, "Password");

        var response = await _authenticationService.AuthenticateAsync(request.Auth);
        return new Response<AuthResponse>(response, "Authenticated!");
    }
}
