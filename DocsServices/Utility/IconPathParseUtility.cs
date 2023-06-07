namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class IconPathParseUtility
{
	public static string? ConvertIconPathToUePath(string iconPath)
	{
		if (iconPath == "None")
		{
			return null;
		}

		if (iconPath.Contains("CoffeeCup"))
			_ = 1;
		
		var text = iconPath.StartsWith("Texture2D") ? iconPath.Remove(0, "Texture2D".Length) : iconPath;
		text = text.Trim().Trim('\'').Trim('"');
		var directoryName = Path.GetDirectoryName(text);
		var text2 = Path.GetFileName(iconPath).Split('.')[0];
		// ReSharper disable once HeapView.ObjectAllocation
		return Path.Combine(directoryName!, text2 + ".png");
	}

	public static string? ReadIconPathFromSchematicIcon(string schematicIcon)
	{
		if (schematicIcon == "None")
		{
			return null;
		}
		
		var num = schematicIcon.IndexOf("Texture2D", StringComparison.Ordinal);
		if (num == -1)
		{
			return null;
		}
		
		var num2 = schematicIcon.IndexOf(',', num);
		if (num2 == -1)
		{
			num2 = schematicIcon.Length;
		}
		
		var iconPath = schematicIcon.Substring(num, num2 - num);
		return ConvertIconPathToUePath(iconPath);
	}

	public static string ReplaceSmallIconPathFromClass(Classes class2)
	{
		var className = class2.ClassName;
		var result = className switch
		{
			"Desc_Wall_Conveyor_8x4_04_C" => "None",
			"Desc_Wall_Conveyor_8x4_04_Steel_C" => "None",
			_ => class2.mSmallIcon
		};

		return result;
	}

	public static string ReplaceBigIconPathFromClass(Classes class2)
	{
		var className = class2.ClassName;
		
		var result = className switch
		{
			"Desc_Wall_Conveyor_8x4_04_C" => "None",
			"Desc_Wall_Conveyor_8x4_04_Steel_C" => "None",
			_ => class2.mPersistentBigIcon
		};

		return result;
	}
}
