namespace SatisfactoryCalculator.Shared.Models;

public class Result<T>
{
	public T Content { get; set; }
	public string Error { get; set; }
	public bool IsSuccess { get; set; }

	public static Result<T> Success(T content)
	{
		return new Result<T>
		{
			Content = content,
			IsSuccess = true
		};
	}

	public static Result<T> Failure(string error)
	{
		return new Result<T>
		{
			Error = error,
			IsSuccess = false
		};
	}
}
