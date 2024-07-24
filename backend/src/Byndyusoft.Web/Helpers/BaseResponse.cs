namespace Byndyusoft.Web.Helpers;

public class BaseResponse<T>
    where T : class
{
    public bool IsError { get; set; }

    public bool IsExceptedError { get; set; }

    public string Message { get; set; }

    public string DefaultMessage { get; set; }

    public T Data { get; set; }


    public BaseResponse(bool isError = false, bool isExceptedError = false, string message = "", string defaultMessage= "", T data = null)
    {
        IsError = isError;
        IsExceptedError = isExceptedError;
        Message = message;
        DefaultMessage = defaultMessage;
        Data = data;
    }
}
