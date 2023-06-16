namespace SatisfactoryCalculator.DocsServices.Utility;

internal class ReleventEventParseUtility
{
    private const string ArrayOpenTag = "(";
    private const string ArraySplitTag = ",";
    private const string ArrayCloseTag = ")";

    public static RelevantEvent[] MapToRelevantEvents(string mRelevantEvents)
    {
        var text = SanitizeInput(mRelevantEvents);
        var sanitizedSplitInput = text.Split(ArraySplitTag);
        // ReSharper disable once HeapView.ObjectAllocation
        return MapToRelevantEventsIEnumerable(sanitizedSplitInput).ToArray();
    }

    private static IEnumerable<RelevantEvent> MapToRelevantEventsIEnumerable(IEnumerable<string> sanitizedSplitInput)
    {
        return sanitizedSplitInput
            // ReSharper disable once ConvertClosureToMethodGroup
            .Select(p => StringToEnumParseUtility.ParseRelevantEventToEnum(p));
    }

    private static string SanitizeInput(string input)
    {
        return input.Replace(ArrayOpenTag, "").Replace(ArrayCloseTag, "");
    }
}
