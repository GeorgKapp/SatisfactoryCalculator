namespace SatisfactoryCalculator.Shared.Utility;

public static class ProgressUtility
{
	public static void ReportOrThrow(string reportMessage, IExtendedProgress<string> progress = null, CancellationToken? token = null)
	{
		token?.ThrowIfCancellationRequested();
		progress?.Report(reportMessage);
	}
}
