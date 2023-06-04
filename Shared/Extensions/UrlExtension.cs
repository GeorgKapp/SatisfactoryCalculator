namespace SatisfactoryCalculator.Shared.Extensions;

public static class UrlExtension
{
	public static string Append(this string baseUrl, string relativePath)
	{
		var baseUrlModified = baseUrl.EndsWith("\\") 
			? baseUrl
			: $"{baseUrl}\\";

		var relativePathModified = relativePath.StartsWith("\\") 
			? relativePath[1..] 
			: relativePath;

		return $"{baseUrlModified}{relativePathModified}";
	}
}
