// ReSharper disable HeapView.ObjectAllocation
namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class ScannableObjectParseUtility
{
	private const string BluePrintTag = "BlueprintGeneratedClass";
	private const string ActorsAllowedToScanTag = "ActorsAllowedToScan";
	private const string ArraySplitTag = ",";

	public static ScannableObject MapToScannableObject(string mScannableObjects)
	{
		var bluePrintTagIndex = mScannableObjects.IndexOf(BluePrintTag, StringComparison.Ordinal) + BluePrintTag.Length;
		var arraySplitTagIndex = mScannableObjects.IndexOf(ArraySplitTag, bluePrintTagIndex, StringComparison.Ordinal);

		var mClassReferenceInput = mScannableObjects.Substring(bluePrintTagIndex, arraySplitTagIndex - bluePrintTagIndex);
		var className = ReferenceParseUtility.GetReferences(mClassReferenceInput).First();

		var startIndex = mScannableObjects.IndexOf(ActorsAllowedToScanTag, StringComparison.Ordinal);
		var actorsAllowedToScan = ReferenceParseUtility.GetReferences(mScannableObjects[startIndex..]);

		return new()
		{
			ItemClass = ClassNameParseUtility.CleanClassName(className),
			ActorsAllowedToScan = actorsAllowedToScan
		};
	}
}
