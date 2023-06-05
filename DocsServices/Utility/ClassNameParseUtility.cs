// ReSharper disable HeapView.ObjectAllocation
namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class ClassNameParseUtility
{
	private static readonly string[] PreFixesArray = {
		"BP", 
		"Build", 
		"CBG", 
		"CustomizerUnlock", 
		"Desc", 
		"Equip", 
		"Ficsmas", 
		"Foundation", 
		"Recipe", 
		"Research",
		"ResourceSink", 
		"Schematic"
	};

	public static string ConvertClassNameToDescClassName(string className)
	{
		foreach (var preFix in PreFixesArray)
		{
			if (className.StartsWith(preFix))
				return $"{Constants.DescriptionPrefix}{className[preFix.Length..]}";
		}

		throw new NotImplementedException("ClassName filtering not implemented: " + className);
	}

	public static string? CleanClassName(string? className)
	{
		className = CorrectClassName(className);
		if (string.IsNullOrEmpty(className))
			return className;

		return className switch
		{
			not null when className.StartsWith("Build_") => className.Replace("Build_", ""),
			not null when className.StartsWith("Desc_") => className.Replace("Desc_", ""),
			not null when className.StartsWith("Equip_") => className.Replace("Equip_", ""),
			not null when className.StartsWith("Schematic_") => className.Replace("Schematic_", ""),
			not null when className.StartsWith("Recipe_") => className.Replace("Recipe_", ""),
			not null when className.StartsWith("ResourceSink_Customizer_") => className.Replace("ResourceSink_Customizer_",""),
			not null when className.StartsWith("ResourceSink_") => className.Replace("ResourceSink_", ""),
			not null when className.StartsWith("BP_EquipmentDescriptor") => className.Replace("BP_EquipmentDescriptor", ""),
			not null when className.StartsWith("BP_") => className.Replace("BP_", ""),
			not null when className.StartsWith("Emote_") => className.Replace("Emote_", ""),
			_ => className
		};
	}

	private static string? CorrectClassName(string? className) => className switch
	{
		"Desc_Geyser_C" => "Desc_NitrogenGas_C",
		"BP_EqDescZipLine_C" => "Equip_Zipline_C",
		"Equip_GasMask_C" => "BP_EquipmentDescriptorGasmask_C",
		"BP_EquipmentDescriptorGasMask_C" => "BP_EquipmentDescriptorGasmask_C",
		"BP_EquipmentDescriptorChainsaw_C" => "Desc_Chainsaw_C",
		"BP_EquipmentDescriptorZipline_C" => "BP_EqDescZipLine_C",
		"BP_EquipmentDescriptorParachute_C" => "Desc_Parachute_C",
		"Desc_RebarGunProjectile_C" => "BP_EquipmentDescriptorRebarGun_Projectile_C",
		"BP_EquipmentDescriptorCandyCane_C" => "BP_EquipmentDescriptorCandyCaneBasher_C",
		"Desc_Wall_Concrete_8x1_FlipTris_C" => "Build_Wall_Concrete_FlipTris_8x1_C",
		"Desc_Wall_Concrete_8x2_FlipTris_C" => "Build_Wall_Concrete_FlipTris_8x2_C",
		"Desc_Wall_Concrete_8x4_FlipTris_C" => "Build_Wall_Concrete_FlipTris_8x4_C",
		"Desc_Wall_Concrete_8x8_FlipTris_C" => "Build_Wall_Concrete_FlipTris_8x8_C",
		"Desc_Wall_Concrete_8x1_Tris_C" => "Build_Wall_Concrete_Tris_8x1_C",
		"Desc_Wall_Concrete_8x2_Tris_C" => "Build_Wall_Concrete_Tris_8x2_C",
		"Desc_Wall_Concrete_8x4_Tris_C" => "Build_Wall_Concrete_Tris_8x4_C",
		"Desc_Wall_Concrete_8x8_Tris_C" => "Build_Wall_Concrete_Tris_8x8_C",
		"Desc_Wall_Orange_8x1_FlipTris_C" => "Build_Wall_Orange_FlipTris_8x1_C",
		"Desc_Wall_Orange_8x2_FlipTris_C" => "Build_Wall_Orange_FlipTris_8x2_C",
		"Desc_Wall_Orange_8x4_FlipTris_C" => "Build_Wall_Orange_FlipTris_8x4_C",
		"Desc_Wall_Orange_8x8_FlipTris_C" => "Build_Wall_Orange_FlipTris_8x8_C",
		"Desc_Wall_Orange_8x1_Tris_C" => "Build_Wall_Orange_Tris_8x1_C",
		"Desc_Wall_Orange_8x2_Tris_C" => "Build_Wall_Orange_Tris_8x2_C",
		"Desc_Wall_Orange_8x4_Tris_C" => "Build_Wall_Orange_Tris_8x4_C",
		"Desc_Wall_Orange_8x8_Tris_C" => "Build_Wall_Orange_Tris_8x8_C",
		"Desc_XMassTree_C" => "Build_XmassTree_C",
		"Desc_WalkwayTurn_C" => "Build_WalkwayTrun_C",
		"Desc_xmassLights_C" => "Build_XmassLightsLine_C",
		"Foundation_ConcretePolished_8x2_C" => "Build_Foundation_ConcretePolished_8x2_2_C",
		"Desc_PowerPoleWallDoubleMk2_C" => "Build_PowerPoleWallDouble_Mk2_C",
		"Desc_PowerPoleWallMk2_C" => "Build_PowerPoleWall_Mk2_C",
		"Desc_PowerPoleWallDoubleMk3_C" => "Build_PowerPoleWallDouble_Mk3_C",
		"Desc_PowerPoleWallMk3_C" => "Build_PowerPoleWall_Mk3_C",
		"Desc_CatwalkTurn_C" => "Build_CatwalkCorner_C",
		"Desc_QuarterPipeMiddle_Ficsit_4x1_C" => "Build_QuarterPipeMiddle_Ficsit_8x1_C",
		"Desc_QuarterPipeMiddle_Ficsit_4x2_C" => "Build_QuarterPipeMiddle_Ficsit_8x2_C",
		"Desc_QuarterPipeMiddle_Ficsit_4x4_C" => "Build_QuarterPipeMiddle_Ficsit_8x4_C",
		_ => className
	};

	public static string? CorrectClassNameForImageLookup(string? className) => className switch
	{
		"Build_PowerPoleWall_Mk3_C" => "Desc_PowerPoleWallMk3_C",
		"Build_PowerPoleWallDouble_Mk3_C" => "Desc_PowerPoleWallDoubleMk3_C",
		"Build_PowerPoleWall_Mk2_C" => "Desc_PowerPoleWallMk2_C",
		"Build_PowerPoleWallDouble_Mk2_C" => "Desc_PowerPoleWallDoubleMk2_C",
		"Build_XmassLightsLine_C" => "Desc_xmassLights_C",
		"Build_WalkwayTrun_C" => "Desc_WalkwayTurn_C",
		"Build_XmassTree_C" => "Desc_XMassTree_C",
		"Build_CatwalkCorner_C" => "Desc_CatwalkTurn_C",
		"Build_Foundation_ConcretePolished_8x2_2_C" => "Foundation_ConcretePolished_8x2_C",
		"Build_Foundation_ConcretePolished_8x4_C" => "Foundation_ConcretePolished_8x4_C",
		"Build_Wall_Concrete_FlipTris_8x1_C" => "Desc_Wall_Concrete_8x1_FlipTris_C",
		"Build_Wall_Concrete_FlipTris_8x2_C" => "Desc_Wall_Concrete_8x2_FlipTris_C",
		"Build_Wall_Concrete_FlipTris_8x4_C" => "Desc_Wall_Concrete_8x4_FlipTris_C",
		"Build_Wall_Concrete_FlipTris_8x8_C" => "Desc_Wall_Concrete_8x8_FlipTris_C",
		"Build_Wall_Concrete_Tris_8x1_C" => "Desc_Wall_Concrete_8x1_Tris_C",
		"Build_Wall_Concrete_Tris_8x2_C" => "Desc_Wall_Concrete_8x2_Tris_C",
		"Build_Wall_Concrete_Tris_8x4_C" => "Desc_Wall_Concrete_8x4_Tris_C",
		"Build_Wall_Concrete_Tris_8x8_C" => "Desc_Wall_Concrete_8x8_Tris_C",
		"Build_Wall_Orange_FlipTris_8x1_C" => "Desc_Wall_Orange_8x1_FlipTris_C",
		"Build_Wall_Orange_FlipTris_8x2_C" => "Desc_Wall_Orange_8x2_FlipTris_C",
		"Build_Wall_Orange_FlipTris_8x4_C" => "Desc_Wall_Orange_8x4_FlipTris_C",
		"Build_Wall_Orange_FlipTris_8x8_C" => "Desc_Wall_Orange_8x8_FlipTris_C",
		"Build_Wall_Orange_Tris_8x1_C" => "Desc_Wall_Orange_8x1_Tris_C",
		"Build_Wall_Orange_Tris_8x2_C" => "Desc_Wall_Orange_8x2_Tris_C",
		"Build_Wall_Orange_Tris_8x4_C" => "Desc_Wall_Orange_8x4_Tris_C",
		"Build_Wall_Orange_Tris_8x8_C" => "Desc_Wall_Orange_8x8_Tris_C",
		"Build_QuarterPipeMiddle_Ficsit_8x1_C" => "Desc_QuarterPipeMiddle_Ficsit_4x1_C",
		"Build_QuarterPipeMiddle_Ficsit_8x2_C" => "Desc_QuarterPipeMiddle_Ficsit_4x2_C",
		"Build_QuarterPipeMiddle_Ficsit_8x4_C" => "Desc_QuarterPipeMiddle_Ficsit_4x4_C",
		_ => className
	};
}
