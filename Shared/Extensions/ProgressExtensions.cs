namespace SatisfactoryCalculator.Shared.Extensions;

public static class ProgressExtensions
{
	public static void ReportErrorMessage(this IExtendedProgress<string> progress, string value)
	{
		progress?.ReportError("ERROR: " + value);
	}

	public static void ReportWarningMessage(this IExtendedProgress<string> progress, string value)
	{
		progress?.ReportWarning("WARNING: " + value);
	}

    public static void ReportSuccessMessage(this IExtendedProgress<string> progress, string value)
    {
        progress?.ReportSuccess("WARNING: " + value);
    }
}
