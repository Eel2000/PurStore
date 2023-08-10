using Newtonsoft.Json;
using PureStore.Domain.Common;

namespace PureStore.API.Middlewares;

public class GlobaleExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "applicationc/json";
            response.StatusCode = StatusCodes.Status500InternalServerError;
            var error = new
            {
                Name = ex.GetType().Name,
                Message = ex.Message,
                Source = ex.Source,
                ErrorCode = StatusCodes.Status500InternalServerError
            };

            var data = new Response<object>(false, error, ex.Message + "\n if that persiste contacts the admin.");

            await response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }
}
