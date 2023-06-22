namespace SatisfactoryCalculator.DocsServices.Extensions;

internal static class DecimalParseExtensions
{
	private static readonly NumberFormatInfo EnglishFormat = new()
	{
		NumberDecimalSeparator = "."
	};

	public static decimal MapToDecimal(this string input)
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

	public static decimal? MapToNullableDecimal(this string input)
	{
		return string.IsNullOrEmpty(input) ? null : MapToDecimal(input);
	}
}
