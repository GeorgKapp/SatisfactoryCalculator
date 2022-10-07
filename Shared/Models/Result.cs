namespace SatisfactoryCalculator.Shared.Models;

public class Result<T>
{
    public T Content { get; set; }
    public string Error { get; set; }
    public bool IsSuccess { get; set; }

    public static Result<T> Success(T content) => new Result<T>
    {
        Content = content,
        IsSuccess = true
    };

    public static Result<T> Failure(string error) => new Result<T>
    {
        Error = error,
        IsSuccess = false
    };
}

public class Result
{
    public string Error { get; set; }
    public bool IsSuccess { get; set; }

    public static Result Success() => new Result
    {
        IsSuccess = true
    };

    public static Result Failure(string error) => new Result
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