namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class StringToEnumParseUtility
{
	public static Form ParseFormStringToEnum(string input) => input switch
	{
		"RF_SOLID" => Form.Solid,
		"RF_LIQUID" => Form.Liquid,
		"RF_GAS" => Form.Gas,
		"RF_INVALID" => Form.Invalid,
		_ => throw new NotImplementedException("Form: " + input),
	};

	public static Form? ParseFormStringToNNullableEnum(string input)
	{
		return string.IsNullOrEmpty(input) 
			? null 
			: ParseFormStringToEnum(input);
	}

	public static StackSize ParseStackSizeStringToEnum(string input) => input switch
	{
		"SS_HUGE" => StackSize.Huge,
		"SS_SMALL" => StackSize.Small,
		"SS_BIG" => StackSize.Big,
		"SS_MEDIUM" => StackSize.Medium,
		"SS_ONE" => StackSize.One,
		"SS_FLUID" => StackSize.Fluid,
		_ => throw new NotImplementedException("StackSize: " + input),
	};

	public static StackSize? ParseStackSizeStringToNullableEnum(string input)
	{
		return string.IsNullOrEmpty(input) 
			? null : ParseStackSizeStringToEnum(input);
	}

    public static EquipmentSlot ParseEquipmentSlotStringToEnum(string input) => input switch
    {
        "ES_ARMS" => EquipmentSlot.Arms,
        "ES_BACK" => EquipmentSlot.Back,
        "ES_BODY" => EquipmentSlot.Body,
        "ES_HEAD" => EquipmentSlot.Head,
        "ES_LEGS" => EquipmentSlot.Legs,
        _ => throw new NotImplementedException("EquipmentSlot: " + input)
    };

    public static EquipmentSlot? ParseEquipmentSlotStringToNullableEnum(string input)
	{
		return string.IsNullOrEmpty(input) 
			? null 
			: ParseEquipmentSlotStringToEnum(input);
	}

	public static SchematicType ParseSchematicTypeStringToEnum(string input) => input switch
	{
		"EST_Custom" => SchematicType.Custom,
		"EST_MAM" => SchematicType.MAM,
		"EST_Tutorial" => SchematicType.Tutorial,
		"EST_HardDrive" => SchematicType.HardDrive,
		"EST_Milestone" => SchematicType.Milestone,
		"EST_Alternate" => SchematicType.Alternate,
		"EST_ResourceSink" => SchematicType.ResourceSink,
		_ => throw new NotImplementedException("SchematicType: " + input),
	};

	public static SchematicType? ParseSchematicTypeStringToNullableEnum(string input)
	{
		return string.IsNullOrEmpty(input) 
			? null 
			: ParseSchematicTypeStringToEnum(input);
	}
}
