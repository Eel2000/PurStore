using MediatR;
using Microsoft.AspNetCore.Mvc;
using PureStore.API.Abstractions;
using PureStore.Application.DTOs.Identity;
using PureStore.Application.Features.Identity.Commands;

namespace PureStore.API.EndPoints.Identity
{
    public class IdentityEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var identityGroup = app.MapGroup("/api/Identity");

            identityGroup.MapPost("/Register", PostRegister);

            identityGroup.MapPost("/Authenticate", AuthenticationPost);
        }

        public async ValueTask<IResult> PostRegister(IMediator mediator, [FromBody] RegisterDTO register)
        {
            var result = await mediator.Send(new RegistrationCommand(register));
            if (result.Succeeded.Value)
                return TypedResults.Ok(result);

            return Results.BadRequest(result);
        }

        public async ValueTask<IResult> AuthenticationPost(IMediator mediator, [FromBody] Auth auth)
        {
            var result = await mediator.Send(new AuthenticationCommand(auth));
            if (result.Succeeded.Value)
                return Results.Ok(result);

            return Results.BadRequest(result);
        }
    }
}
