using MediatR;
using PureStore.Application.DTOs.Identity;
using PureStore.Application.Interfaces.Identity;
using PureStore.Domain.Common;

namespace PureStore.Application.Features.Identity.Commands;

public class RegistrationCommand : IRequest<Response<object>>
{
    public RegisterDTO Register { get; set; }

    public RegistrationCommand(RegisterDTO register)
    {
        Register = register;
    }
}

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, Response<object>>
{
    private readonly IAuthenticationService _authenticationService;

    public RegistrationCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Response<object>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        ArgumentException.ThrowIfNullOrEmpty(request.Register.Email, "Email");
        ArgumentException.ThrowIfNullOrEmpty(request.Register.Username, "Username");
        ArgumentException.ThrowIfNullOrEmpty(request.Register.Password, "Password");

        var response = await _authenticationService.RegisterAsync(request.Register);
        return new Response<object>(response, "Registration completed successfully!");
    }
}
