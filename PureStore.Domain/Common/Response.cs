namespace PureStore.Domain.Common;

public class Response<T>
{
    public T? Data { get; set; }
    public bool? Succeeded { get; set; }
    public string[]? Errors { get; set; }
    public string? Message { get; set; }

    public Response()
    {
    }

    public Response(T data)
    {
        Succeeded = true;
        Message = string.Empty;
        Data = data;
    }

    public Response(string Data)
    {
        Succeeded = false;
        Message = Data;
    }

    public Response(T data, string meessage = "", string[]? errors = default)
    {
        Succeeded = true;
        Message = meessage;
        Errors = errors;
        Data = data;
    }
}
