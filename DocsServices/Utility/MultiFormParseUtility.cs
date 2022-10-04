namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class MultiFormParseUtility
{
	private const string ArrayOpenTag = "(";
	private const string ArraySplitTag = ",";
	private const string ArrayCloseTag = ")";

	public static Form[] MapToForms(string mAllowedResourceForms)
	{
		string text = SanitizeInput(mAllowedResourceForms);
		string[] sanitizedSplitInput = text.Split(ArraySplitTag);
		return MapToFormsIEnumerable(sanitizedSplitInput).ToArray();
	}

	private static IEnumerable<Form> MapToFormsIEnumerable(string[] sanitizedSplitInput)
	{
		for (int i = 0; i < sanitizedSplitInput.Length; i++)
		{
			yield return StringToEnumParseUtility.ParseFormStringToEnum(sanitizedSplitInput[i]);
		}
	}

	private static string SanitizeInput(string input)
	{
		return input.Replace(ArrayOpenTag, "").Replace(ArrayCloseTag, "");
	}
}
