namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class ReferenceParseUtility
{
	private const string ArrayOpenTag = "(";
	private const string ArraySplitTag = ",";
	private const string ArrayCloseTag = ")";
	private const string ClassReferenceOpenTag = "'\"";
	private const string ClassReferenceSplitTag = ".";
	private const string ClassReferenceCloseTag = "\"'";
	private const string EqualSplitTag = "=";
	private const string AmountTag = "Amount";
	private const string ResourceNodeTypeTag = "ResourceNodeType";

    public static string[] MapToReferenceArray(string mClassReferenceInput)
	{
		if (string.IsNullOrEmpty(mClassReferenceInput))
			return Array.Empty<string>();

		var sanitizedInput = SanitizeInput(mClassReferenceInput);
		var sanitizedSplitInput = sanitizedInput.Split(ArraySplitTag);

		// ReSharper disable once HeapView.ObjectAllocation
		return MapToReferenceIEnumerable(sanitizedSplitInput)
			.ToArray();
	}

	public static string[] MapToReferenceArrayIgnoreIdentifiers(string mClassReferenceInput)
	{
		if (string.IsNullOrEmpty(mClassReferenceInput))
			return Array.Empty<string>();

		var sanitizedInput = SanitizeInput(mClassReferenceInput);
		var sanitizedSplitInput = sanitizedInput.Split(ArraySplitTag);

		// ReSharper disable once HeapView.ObjectAllocation
		return MapToReferenceIEnumerable(sanitizedSplitInput)
			.Where(p => !p.StartsWith(ResourceNodeTypeTag) && !p.StartsWith(AmountTag))
			.ToArray();
	}

	private static IEnumerable<string> MapToReferenceIEnumerable(string[] sanitizedSplitInput)
	{
		for (var i = 0; i < sanitizedSplitInput.Length; i++)
			yield return MapToClassReference(sanitizedSplitInput[i]);
	}

	private static string MapToClassReference(string input)
	{
		return ClassNameParseUtility.CleanClassName(SanitizeItemName(input))!;
	}

	public static Reference[] MapToReferenceWithAmountArray(string mClassReferenceWithAmountInput)
	{
		if (string.IsNullOrEmpty(mClassReferenceWithAmountInput))
			return Array.Empty<Reference>();

		var sanitizedInput = SanitizeInput(mClassReferenceWithAmountInput);
		var sanitizedSplitInput = sanitizedInput.Split(ArraySplitTag);

		return MapToReferenceWithAmountIEnumerable(sanitizedSplitInput)
			.ToArray();
	}

	private static IEnumerable<Reference> MapToReferenceWithAmountIEnumerable(string[] sanitizedSplitInput)
	{
		for (int i = 0; i < sanitizedSplitInput.Length; i += 2)
			yield return MapToReferenceWithAmount(sanitizedSplitInput[i], sanitizedSplitInput[i + 1]);
	}

	private static Reference MapToReferenceWithAmount(string nameInput, string amountInput)
	{
		if (!amountInput.StartsWith(AmountTag))
			throw new NotImplementedException("Case not implemented for scraping missing amount in class reference");

		var className = SanitizeItemName(nameInput);
		var amount = SanitizeAmount(amountInput);
		return new(className: ClassNameParseUtility.CleanClassName(className)!, amount: amount);
	}

	private static string SanitizeInput(string input)
	{
		return input.Replace(ArrayOpenTag, "").Replace(ArrayCloseTag, "");
	}

	private static string SanitizeItemName(string input)
	{
		var source = input.Replace(ClassReferenceCloseTag, "").Split(ClassReferenceOpenTag);
		return source.Last().Split(ClassReferenceSplitTag).Last();
	}

	private static double SanitizeAmount(string input)
	{
		var source = input.Split(EqualSplitTag);
		return NumberParseUtility.MapToDouble(source.Last());
	}
}
