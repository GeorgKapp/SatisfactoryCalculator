// ReSharper disable HeapView.ObjectAllocation

using Microsoft.EntityFrameworkCore;

namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class ScannableObjectParseUtility
{
	private const string BluePrintTag = "BlueprintGeneratedClass";
	private const string ActorsAllowedToScanTag = "ActorsAllowedToScan";
	private const string ArraySplitTag = ",";
	
	private static readonly string[] HostileCreaturesTags = new string[]
	{
		"HogAlpha",
		"Hog",
		"SpitterDesert",
		"SpitterAqua",
		"SpitterForest",
		"SpitterRForest",
		"SpitterDesertAlpha",
		"SpitterAquaAlpha",
		"SpitterForestAlpha",
		"SpitterRForestAlpha",
		"Hatcher",
		"ArachnophobiaStingerChild",
		"ArachnophobiaStinger",
		"ArachnophobiaStingerElite", 
		"StingerChild",
		"Stinger", 
		"StingerElite"
	};

	public static ScannableObject[] MapToScannableObjects(string mScannableObjects, string[] itemClassNames, string[] buildingClassNames)
	{
		var bluePrintTagIndex = mScannableObjects.IndexOf(BluePrintTag, StringComparison.Ordinal) + BluePrintTag.Length;
		var arraySplitTagIndex = mScannableObjects.IndexOf(ArraySplitTag, bluePrintTagIndex, StringComparison.Ordinal);

		var mClassReferenceInput = mScannableObjects.Substring(bluePrintTagIndex, arraySplitTagIndex - bluePrintTagIndex);
		var className = ReferenceParseUtility.GetReferences(mClassReferenceInput).First();

		var startIndex = mScannableObjects.IndexOf(ActorsAllowedToScanTag, StringComparison.Ordinal);
		var scanningActorsReferences = ReferenceParseUtility.GetReferences(mScannableObjects[startIndex..])
			.Select(p => ClassNameParseUtility.CorrectClassNameForSchematics(p)).ToArray();

		var scanningActors = new List<ScanningActor>();
		foreach (var scanningActorsReference in scanningActorsReferences)
		{
			if(itemClassNames.Contains(scanningActorsReference)) {
				scanningActors.Add(new() { ItemClassName = scanningActorsReference });
			}
			else if (buildingClassNames.Contains(scanningActorsReference)) {
				scanningActors.Add(new() { BuildingClassName = scanningActorsReference });
			}
			else
				throw new($"Scanning Actor Reference was not found: {scanningActorsReference}");
		}
		
		if (className == "HostileCreature")
		{
			return HostileCreaturesTags
				.Select(p => new ScannableObject { CreatureClassName = p, ScanningActors = scanningActors})
				.ToArray();
		}

		if (itemClassNames.Contains(className))
		{
			return new[] { new ScannableObject
			{
				ItemClassName = className,
				ScanningActors = scanningActors
			}};
		}

		return new[] { new ScannableObject
		{
			PlantClassName = className,
			ScanningActors = scanningActors
		}};
	}
}
