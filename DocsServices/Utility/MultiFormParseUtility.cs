namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class MultiFormParseUtility
{
	private const string ArrayOpenTag = "(";
	private const string ArraySplitTag = ",";
	private const string ArrayCloseTag = ")";

	public static Form[] MapToForms(string mAllowedResourceForms)
	{
		var text = SanitizeInput(mAllowedResourceForms);
		var sanitizedSplitInput = text.Split(ArraySplitTag);
		// ReSharper disable once HeapView.ObjectAllocation
		return MapToFormsIEnumerable(sanitizedSplitInput).ToArray();
	}

	private static IEnumerable<Form> MapToFormsIEnumerable(IEnumerable<string> sanitizedSplitInput)
	{
		return sanitizedSplitInput
			// ReSharper disable once ConvertClosureToMethodGroup
			.Select(p => StringToEnumParseUtility.ParseFormStringToEnum(p));
	}

	private static string SanitizeInput(string input)
	{
		return input.Replace(ArrayOpenTag, "").Replace(ArrayCloseTag, "");
	}
}
