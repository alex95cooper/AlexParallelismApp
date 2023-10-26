namespace AlexParallelismApp.Domain;

public class Result : IResult
{
    public ErrorStatus ErrorStatus { get; init; }
    public string Error { get; init; }
    public bool IsSuccess { get; init; }
}

public class Result<T> : Result, IResult<T>
{
    public T Data { get; init; }
}