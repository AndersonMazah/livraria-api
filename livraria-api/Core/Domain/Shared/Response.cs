namespace Livraria.Core.Domain.Shared;

public class Response<T> where T : class
{
    public bool Success { get; private set; } = false;

    public ErrorCodes ErrorCode { get; private set; }

    public string Message { get; private set; } = string.Empty;

    public T Data { get; private set; } = null!;

    public Response()
    {
        Success = true;
    }

    public Response(T data)
    {
        Success = true;
        Data = data;
    }

    public Response(ErrorCodes errorCodes, string message)
    {
        Success = false;
        ErrorCode = errorCodes;
        Message = message;
    }

}