namespace SatisfactoryCalculator.Shared.Extensions;

public static class StringExtensions
{
	public static string RemoveWhitespace(this string input) => new string(input.Where(p => !char.IsWhiteSpace(p)).ToArray());
	public static void CreateDirectoryIfNotExists(this string path)
	{
		if (!Directory.Exists(path))
			Directory.CreateDirectory(path);
	}
}
