namespace SatisfactoryCalculator.Shared.Common.Progress;

public interface IExtendedProgress<T> : IProgress<T>
{
    /// <summary>
    /// Reports an error progress update.
    /// </summary>
    /// <param name="value">The value of the updated progress.</param>
    void ReportError(T value);

    /// <summary>
    /// Reports a warning progress update.
    /// </summary>
    /// <param name="value">The value of the updated progress.</param>
    void ReportWarning(T value);

    /// <summary>
    /// Reports a success progress update.
    /// </summary>
    /// <param name="value">The value of the updated progress.</param>
    void ReportSuccess(T value);

    /// <summary>
    /// Reports a progress update.
    /// </summary>
    /// <param name="value">The value of the updated progress.</param>
    /// <param name="token">The cancellationToken that throws an error if it has been cancelled.</param>
    void ReportOrThrow(T value, CancellationToken? token = null);
}
