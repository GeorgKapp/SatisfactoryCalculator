namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class NumberParseUtility
{
	private static readonly NumberFormatInfo EnglishFormat = new()
	{
		NumberDecimalSeparator = "."
	};

	public static double MapToDouble(string input)
	{
		try
		{
			return Convert.ToDouble(input, EnglishFormat);
		}
		catch
		{
			throw new ArgumentException("Double Input: " + input + " is not a valid value");
		}
	}

	public static double? MapToNullableDouble(string input)
	{
		return string.IsNullOrEmpty(input) ? null : MapToDouble(input);
	}
	
	public static decimal MapToDecimal(string input)
	{
		try
		{
			return Convert.ToDecimal(input, EnglishFormat);
		}
		catch
		{
			throw new ArgumentException("Double Input: " + input + " is not a valid value");
		}
	}

	public static decimal? MapToNullableDecimal(string input)
	{
		return string.IsNullOrEmpty(input) ? null : MapToDecimal(input);
	}
}
