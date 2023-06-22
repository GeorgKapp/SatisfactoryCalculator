namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class MultiRelevantEventsParseUtility
{
	private const string ArrayOpenTag = "(";
	private const string ArraySplitTag = ",";
	private const string ArrayCloseTag = ")";

	public static RelevantEvent[] MapToRelevantEvents(string mAllowedResourceForms)
	{
		var text = SanitizeInput(mAllowedResourceForms);
		var sanitizedSplitInput = text.Split(ArraySplitTag);
		// ReSharper disable once HeapView.ObjectAllocation
		return MapToRelevantEventsIEnumerable(sanitizedSplitInput).ToArray();
	}

	private static IEnumerable<RelevantEvent> MapToRelevantEventsIEnumerable(IEnumerable<string> sanitizedSplitInput)
	{
		return sanitizedSplitInput
			// ReSharper disable once ConvertClosureToMethodGroup
			.Select(p => p.ParseToRelevantEvent());
	}

	private static string SanitizeInput(string input)
	{
		return input.Replace(ArrayOpenTag, "").Replace(ArrayCloseTag, "");
	}
}
