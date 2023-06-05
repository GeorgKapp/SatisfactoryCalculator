namespace SatisfactoryCalculator.Shared.Common.Progress;

public sealed class ExtendedProgress<T> : IExtendedProgress<T>
{
    public ExtendedProgress(Action<ReportState<T>> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        _handler = handler;
        _synchronizationContext = SynchronizationContext.Current ?? ProgressStatics.DefaultContext;
        _invokeHandlers = InvokeHandlers;
    }

    public void Report(T value) =>
        OnReport(new() { Value = value });
    
    public void ReportSuccess(T value) =>
        OnReport(new() { Value = value });

    public void ReportOrThrow(T value, CancellationToken? token = null)
    {
        token?.ThrowIfCancellationRequested();
        Report(value);
    }

    private void OnReport(ReportState<T> value)
    {
        if (_handler is not null)
            _synchronizationContext.Post(_invokeHandlers, value);
    }

    private void InvokeHandlers(object? state)
    {
        var value = (ReportState<T>)state!;

        _handler?.Invoke(value);
    }

    private readonly SynchronizationContext _synchronizationContext;
    private readonly Action<ReportState<T>>? _handler;
    private readonly SendOrPostCallback _invokeHandlers;
}