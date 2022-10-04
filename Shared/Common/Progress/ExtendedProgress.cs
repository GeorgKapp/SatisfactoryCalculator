namespace SatisfactoryCalculator.Shared.Common.Progress;

public class ExtendedProgress<T> : IExtendedProgress<T>
{
    /// <summary>Raised for each reported progress value.</summary>
    /// <remarks>
    /// Handlers registered with this event will be invoked on the
    /// <see cref="SynchronizationContext"/> captured when the instance was constructed.
    /// </remarks>
    public event EventHandler<ReportState<T>>? ProgressChanged;

    public ExtendedProgress()
    {
        _synchronizationContext = SynchronizationContext.Current ?? ProgressStatics.DefaultContext;
        _invokeHandlers = new SendOrPostCallback(InvokeHandlers);
    }

    public ExtendedProgress(Action<ReportState<T>> handler) : this()
    {
        ArgumentNullException.ThrowIfNull(handler);
        _handler = handler;
    }

    public void Report(T value) =>
        OnReport(new ReportState<T> { Value = value, ProgressState = ProgressState.Normal });

    public void ReportError(T value) =>
        OnReport(new ReportState<T> { Value = value, ProgressState = ProgressState.Error });

    public void ReportWarning(T value) => 
        OnReport(new ReportState<T> { Value = value, ProgressState = ProgressState.Warning });

    public void ReportSuccess(T value) =>
        OnReport(new ReportState<T> { Value = value, ProgressState = ProgressState.Success });

    protected virtual void OnReport(ReportState<T> value)
    {
        Action<ReportState<T>>? handler = _handler;
        EventHandler<ReportState<T>>? changedEvent = ProgressChanged;

        if (handler is not null || changedEvent is not null)
            _synchronizationContext.Post(_invokeHandlers, value);
    }

    private void InvokeHandlers(object? state)
    {
        ReportState<T> value = (ReportState<T>)state!;

        Action<ReportState<T>>? handler = _handler;
        EventHandler<ReportState<T>>? changedEvent = ProgressChanged;

        handler?.Invoke(value);
        changedEvent?.Invoke(this, value);
    }

    private readonly SynchronizationContext _synchronizationContext;
    private readonly Action<ReportState<T>>? _handler;
    private readonly SendOrPostCallback _invokeHandlers;
}