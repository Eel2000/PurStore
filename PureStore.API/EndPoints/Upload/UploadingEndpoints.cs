using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using PureStore.API.Abstractions;
using PureStore.Application.DTOs.UploadApps;
using PureStore.Application.Features.Uploads.Commands;
using PureStore.Application.Features.Uploads.Queries;

namespace PureStore.API.EndPoints.Upload
{
    public class UploadingEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var baseGroup = app.MapGroup("/api/Uploading");

            baseGroup.MapGet("/applications", GetListApp);

            baseGroup.MapGet("/download", GetDownload);

            baseGroup.MapPost("/Upload", UploadAppPost).RequireAuthorization(d =>
            {
                d.RequireAuthenticatedUser();
                d.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
            });

            baseGroup.MapDelete("/remove", DeleteApp).RequireAuthorization(d =>
            {
                d.RequireAuthenticatedUser();
                d.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
            });

        }

        public async ValueTask<IResult> UploadAppPost(IMediator mediator, [FromBody] UploadApp app)
        {
            var result = await mediator.Send(new UploadAppCommand(app));
            if (result.Succeeded.Value)
                return TypedResults.CreatedAtRoute(result);

            return TypedResults.BadRequest(result);
        }

        public async ValueTask<IResult> DeleteApp(IMediator mediator, [FromQuery] string applicationId)
        {
            var result = await mediator.Send(new RemoveAppCommand(applicationId));
            if (result.Succeeded.Value)
                return TypedResults.Ok(result);

            return TypedResults.BadRequest(result);
        }

        public async ValueTask<IResult> GetDownload(IMediator mediator, [FromQuery] string applicationId)
        {
            var result = await mediator.Send(new DownloadQuery(applicationId));

            return TypedResults.Ok(result);
        }

        public async ValueTask<IResult> GetListApp(IMediator mediator)
        {
            var result = await mediator.Send(new GetAllAppQueries());

            return TypedResults.Ok(result);
        }

        public async ValueTask<IResult> GetListPaged(IMediator mediator, [FromQuery] int page, [FromQuery] int size)
        {
            var result = await mediator.Send(new GetPagedDataQuery(page, size));

            return TypedResults.Ok(result);
        }
    }
}
