namespace SatisfactoryCalculator.Shared.Models;

public class Result<T>
{
    public T? Content { get; private init; }
    public string? Error { get; private init; }
    public bool IsSuccess { get; private init; }

    public static Result<T> Success(T content) => new()
    {
        Content = content,
        IsSuccess = true
    };

    public static Result<T> Failure(string error) => new()
    {
        Error = error,
        IsSuccess = false
    };
}

public class Result
{
    public string? Error { get; private init; }
    public bool IsSuccess { get; private init; }

    public static Result Success() => new()
    {
        IsSuccess = true
    };

    public static Result Failure(string error) => new()
    {
        Error = error,
        IsSuccess = false
    };

    public static Result Combine(IEnumerable<Result> results)
    {
        var errorResults = results
            .Where(p => !p.IsSuccess)
            .ToArray();

        return errorResults.Any() 
            ? Failure(string.Join(Environment.NewLine, errorResults.Select(p => p.Error))) 
            : Success();
    }
}