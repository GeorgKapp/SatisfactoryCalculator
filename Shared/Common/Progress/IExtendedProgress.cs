namespace SatisfactoryCalculator.Shared.Common.Progress;

public interface IExtendedProgress<in T> : IProgress<T>
{
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
