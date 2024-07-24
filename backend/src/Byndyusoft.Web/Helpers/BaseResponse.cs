namespace Byndyusoft.Web.Helpers;

public class BaseResponse<T>
    where T : class
{
    public bool IsError { get; set; }

    public bool IsExceptedError { get; set; }

    public string Message { get; set; }

    public string DefaultMessage { get; set; }

    public T Data { get; set; }

    public BaseResponse(bool isError, string message)
    {
        IsError = isError;
        Message = message;
    }

    public BaseResponse(bool isError, bool isExceptedError, string defaultMessage)
    {
        IsError = isError;
        IsExceptedError = isExceptedError;
        DefaultMessage = defaultMessage;
    }

    public BaseResponse(T data)
    {
        Data = data;
    }
}
