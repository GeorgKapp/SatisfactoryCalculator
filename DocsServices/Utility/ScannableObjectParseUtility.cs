namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class ScannableObjectParseUtility
{
	private const string BluePrintTag = "BlueprintGeneratedClass";
	private const string ActorsAllowedToScanTag = "ActorsAllowedToScan";
	private const string ArraySplitTag = ",";

	public static ScannableObject MapToScannableObject(string mScannableObjects)
	{
		int bluePrintTagIndex = mScannableObjects.IndexOf(BluePrintTag) + BluePrintTag.Length;
		int arraySplitTagIndex = mScannableObjects.IndexOf(ArraySplitTag, bluePrintTagIndex);

		string mClassReferenceInput = mScannableObjects.Substring(bluePrintTagIndex, arraySplitTagIndex - bluePrintTagIndex);
		string className = ReferenceParseUtility.MapToReferenceArray(mClassReferenceInput).First();

		int startIndex = mScannableObjects.IndexOf(ActorsAllowedToScanTag);
		string[] actorsAllowedToScan = ReferenceParseUtility.MapToReferenceArray(mScannableObjects.Substring(startIndex));

		return new ScannableObject
		{
			ItemClass = ClassNameParseUtility.CleanClassName(className),
			ActorsAllowedToScan = actorsAllowedToScan
		};
	}
}
