namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class IconPathParseUtility
{
	public static string ConvertIconPathToUEPath(string iconPath)
	{
		if (iconPath == "None")
		{
			return null;
		}

		if (iconPath.Contains("CoffeeCup"))
			_ = 1;
		
		string text = (iconPath.StartsWith("Texture2D") ? iconPath.Remove(0, "Texture2D".Length) : iconPath);
		text = text.Trim().Trim('\'').Trim('"');
		string directoryName = Path.GetDirectoryName(text);
		string text2 = Path.GetFileName(iconPath)!.Split('.')[0];
		return Path.Combine(directoryName, text2 + ".png");
	}

	public static string ReadIconPathFromSchematicIcon(string schematicIcon)
	{
		if (schematicIcon == "None")
		{
			return null;
		}
		int num = schematicIcon.IndexOf("Texture2D");
		if (num == -1)
		{
			return null;
		}
		int num2 = schematicIcon.IndexOf(',', num);
		if (num2 == -1)
		{
			num2 = schematicIcon.Length;
		}
		string iconPath = schematicIcon.Substring(num, num2 - num);
		return ConvertIconPathToUEPath(iconPath);
	}

	public static string? ReplaceSmallIconPathFromClass(Class2 class2)
	{
		string className = class2.ClassName;
		
		var result = className switch
		{
			"Desc_Wall_Conveyor_8x4_04_C" => "None",
			"Desc_Wall_Conveyor_8x4_04_Steel_C" => "None",
			_ => class2.mSmallIcon
		};

		return result;
	}

	public static string? ReplaceBigIconPathFromClass(Class2 class2)
	{
		string className = class2.ClassName;
		
		var result = className switch
		{
			"Desc_Wall_Conveyor_8x4_04_C" => "None",
			"Desc_Wall_Conveyor_8x4_04_Steel_C" => "None",
			_ => class2.mPersistentBigIcon
		};

		return result;
	}

	private static bool IsIconEmpty(string icon)
	{
		if (!string.IsNullOrEmpty(icon) && !icon.Contains("01_White") && (icon != "None"))
		{
			throw new ArgumentException("Icon not empty: " + icon);
		}
		return true;
	}
}
