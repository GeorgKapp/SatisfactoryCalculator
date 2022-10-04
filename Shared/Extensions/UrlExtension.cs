namespace SatisfactoryCalculator.Shared.Extensions;

public static class UrlExtension
{
	public static string Combine(this string baseUrl, string relativePath) => new Uri(new Uri(baseUrl), relativePath).AbsoluteUri;
	public static string Append(this string baseUrl, string relativePath)
	{
		string baseUrlModified = baseUrl.EndsWith("\\") 
			? baseUrl
			: baseUrl + "\\";

		string relativePathModified = relativePath.StartsWith("\\") 
			? relativePath.Substring(1) 
			: relativePath;

		return baseUrlModified + relativePathModified;
	}
}
