// ReSharper disable HeapView.ObjectAllocation
namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class ScannableObjectParseUtility
{
	private const string BluePrintTag = "BlueprintGeneratedClass";
	private const string ActorsAllowedToScanTag = "ActorsAllowedToScan";
	private const string ArraySplitTag = ",";
	
	private static readonly string[] HostileCreaturesTags = new string[]
	{
		"HogAlpha_C",
		"Hog_C",
		"SpitterDesert_C",
		"SpitterAqua_C",
		"SpitterForest_C",
		"SpitterRForest_C",
		"SpitterDesertAlpha_C",
		"SpitterAquaAlpha_C",
		"SpitterForestAlpha_C",
		"SpitterRForestAlpha_C",
		"Hatcher_C",
		"ArachnophobiaStingerChild_C",
		"ArachnophobiaStinger_C",
		"ArachnophobiaStingerElite_C", 
		"StingerChild_C",
		"Stinger_C", 
		"StingerElite_C"
	};

	public static ScannableObject[] MapToScannableObjects(string mScannableObjects)
	{
		var bluePrintTagIndex = mScannableObjects.IndexOf(BluePrintTag, StringComparison.Ordinal) + BluePrintTag.Length;
		var arraySplitTagIndex = mScannableObjects.IndexOf(ArraySplitTag, bluePrintTagIndex, StringComparison.Ordinal);

		var mClassReferenceInput = mScannableObjects.Substring(bluePrintTagIndex, arraySplitTagIndex - bluePrintTagIndex);
		var className = ReferenceParseUtility.GetReferences(mClassReferenceInput).First();

		var startIndex = mScannableObjects.IndexOf(ActorsAllowedToScanTag, StringComparison.Ordinal);
		var actorsAllowedToScan = ReferenceParseUtility.GetReferences(mScannableObjects[startIndex..])
			.Select(p => ClassNameParseUtility.CorrectClassNameForSchematics(p)).ToArray();

		if (className == "HostileCreature_C")
		{
			return HostileCreaturesTags
				.Select(p => new ScannableObject{ ItemClass = p, ActorsAllowedToScan = actorsAllowedToScan })
				.ToArray();
		}
		
		return new[] { new ScannableObject
		{
			ItemClass = className,
			ActorsAllowedToScan = actorsAllowedToScan
		}};
	}
}
