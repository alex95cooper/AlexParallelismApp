namespace AlexParallelismApp.Domain;

public interface IResult
{
    ErrorStatus ErrorStatus { get; }
    string Error { get; }
    bool IsSuccess { get; }
}

public interface IResult<out T> : IResult
{
    T Data { get; }
}