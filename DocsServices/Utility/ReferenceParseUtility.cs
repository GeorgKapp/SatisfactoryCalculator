using SatisfactoryCalculator.DocsServices.Utility.Parser;

namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class ReferenceParseUtility
{
	private const string ArrayOpenTag = "(";
	private const string ArraySplitTag = ",";
	private const string ArrayCloseTag = ")";
	private const string ClassReferenceOpenTag = "'\"";
	private const string ClassReferenceSplitTag = ".";
	private const string ClassReferenceCloseTag = "\"'";

	public static string[] GetReferences(string mClassReferenceInput)
	{
		if (string.IsNullOrEmpty(mClassReferenceInput))
			return Array.Empty<string>();

		var sanitizedInput = SanitizeInput(mClassReferenceInput);
		var sanitizedSplitInput = sanitizedInput.Split(ArraySplitTag);

		// ReSharper disable once HeapView.ObjectAllocation
		return MapToReferenceIEnumerable(sanitizedSplitInput)
			.ToArray();
	}
    
	private static IEnumerable<string> MapToReferenceIEnumerable(IEnumerable<string> sanitizedSplitInput)
	{
		return sanitizedSplitInput.Select(p => 
			// ReSharper disable once ConvertClosureToMethodGroup
			MapToClassReference(p));
	}

	private static string MapToClassReference(string input)
	{
		return ClassNameParseUtility.CleanClassName(SanitizeItemName(input))!;
	}

	public static Reference[] GetReferencesWithAmount(string mClassReferenceWithAmountInput)
	{
		if (string.IsNullOrEmpty(mClassReferenceWithAmountInput))
			return Array.Empty<Reference>();

		var result = UnrealEngineClassParser.ParseInputs(mClassReferenceWithAmountInput);
		
		return result
			.Select(p => new Reference(p.ClassName, p.Amount ?? 1))
			.ToArray();
	}

	private static string SanitizeInput(string input)
	{
		return input
			.Replace(ArrayOpenTag, "")
			.Replace(ArrayCloseTag, "");
	}

	private static string SanitizeItemName(string input)
	{
		var source = input
			.Replace(ClassReferenceCloseTag, "")
			.Split(ClassReferenceOpenTag);
		
		return source.
			Last()
			.Split(ClassReferenceSplitTag)
			.Last();
	}
}
