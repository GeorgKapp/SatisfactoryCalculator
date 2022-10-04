namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class NumberParseUtility
{
	private static NumberFormatInfo _englishFormat = new NumberFormatInfo
	{
		NumberDecimalSeparator = "."
	};

	public static double MapToDouble(string input)
	{
		try
		{
			return Convert.ToDouble(input, _englishFormat);
		}
		catch
		{
			throw new ArgumentException("Double Input: " + input + " is not a valid value");
		}
	}

	public static double? MapToNullableDouble(string input)
	{
		return string.IsNullOrEmpty(input) ? null : new double?(MapToDouble(input));
	}
}
