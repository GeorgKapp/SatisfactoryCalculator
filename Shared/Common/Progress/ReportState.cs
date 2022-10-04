namespace SatisfactoryCalculator.Shared.Common.Progress;

public class ReportState<T>
{
    public ProgressState ProgressState { get; init; }
    public T? Value { get; init; }
}