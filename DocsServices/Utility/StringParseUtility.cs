namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class StringParseUtility
{
	public static string GetClassNameWithoutTagPrefix(string input)
	{
		int startIndex = input.IndexOf("_") + 1;
		return input.Substring(startIndex);
	}
}
