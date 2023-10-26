namespace AlexParallelismApp.Domain;

public static class ResultCreator
{
    public static Result<T> GetInvalidResult<T>(string message, ErrorStatus status)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = message,
            ErrorStatus = status
        };
    }

    public static Result GetInvalidResult(string message, ErrorStatus status)
    {
        return new Result
        {
            IsSuccess = false,
            Error = message,
            ErrorStatus = status
        };
    }

    public static Result<T> GetValidResult<T>(T data)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data
        };
    }

    public static Result GetValidResult()
    {
        return new Result
        {
            IsSuccess = true,
        };
    }
}