namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class ClassNameParseUtility
{
	private static string[] preFixesArray = new string[12]
	{
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
		for (int i = 0; i < preFixesArray.Length; i++)
		{
			if (className.StartsWith(preFixesArray[i]))
				return Constants.DescriptionPrefix + className.Substring(preFixesArray[i].Length);
		}
		throw new NotImplementedException("ClassName filtering not implemented: " + className);
	}

	public static string ConvertClassNameToEquipmentClassName(string className)
	{
		if (className.StartsWith(Constants.BlueprintEquipmentDescriptorPrefix))
			return "Equip_" + className.Substring(Constants.BlueprintEquipmentDescriptorPrefix.Length);

		for (int i = 0; i < preFixesArray.Length; i++)
		{
			if (className.StartsWith(preFixesArray[i]))
				return "Equip" + className.Substring(preFixesArray[i].Length);
		}
		throw new NotImplementedException("ClassName filtering not implemented: " + className);
	}

	public static string ConvertClassNameToEquipmentDescriptorClassName(string className)
	{
		for (int i = 0; i < preFixesArray.Length; i++)
		{
			if (className.StartsWith(preFixesArray[i]))
				return Constants.BlueprintEquipmentDescriptorPrefix + className.Substring(preFixesArray[i].Length + 1);
		}
		throw new NotImplementedException("ClassName filtering not implemented: " + className);
	}

	public static string CleanClassName(string className)
	{
		className = CorrectClassName(className);
		if (string.IsNullOrEmpty(className))
			return className;

		switch (className)
		{
            case { } when className.StartsWith("Build_"): return className.Replace("Build_", "");
            case { } when className.StartsWith("Desc_"): return className.Replace("Desc_", "");
            case { } when className.StartsWith("Equip_"): return className.Replace("Equip_", "");
            case { } when className.StartsWith("Schematic_"): return className.Replace("Schematic_", "");
            case { } when className.StartsWith("Recipe_"): return className.Replace("Recipe_", "");
            case { } when className.StartsWith("ResourceSink_Customizer_"): return className.Replace("ResourceSink_Customizer_", "");
            case { } when className.StartsWith("ResourceSink_"): return className.Replace("ResourceSink_", "");
            case { } when className.StartsWith("BP_EquipmentDescriptor"): return className.Replace("BP_EquipmentDescriptor", "");
            case { } when className.StartsWith("BP_"): return className.Replace("BP_", "");
            case { } when className.StartsWith("Emote_"): return className.Replace("Emote_", "");
			default: return className;
		}
	}

	public static string CorrectClassName(string className) => className switch
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
		_ => className,
	};

	public static string CorrectClassNameForImageLookup(string className) => className switch
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
		_ => className,
	};
}
