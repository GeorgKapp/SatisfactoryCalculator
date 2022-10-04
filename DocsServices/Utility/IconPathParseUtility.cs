namespace SatisfactoryCalculator.DocsServices.Utility;

internal static class IconPathParseUtility
{
	public static string ConvertIconPathToUEPath(string iconPath)
	{
		if (iconPath == "None")
		{
			return null;
		}
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
		if (1 == 0)
		{
		}
		string result = ((className == "Desc_Wall_Conveyor_8x4_04_C") ? (IsIconEmpty(class2.mSmallIcon) ? "\\Game\\FactoryGame\\Buildable\\Building\\Wall\\UI\\Wall_Conveyor_Perpendicular_Orange_256.png" : null) : ((!(className == "Desc_Wall_Conveyor_8x4_04_Steel_C")) ? class2.mSmallIcon : (IsIconEmpty(class2.mSmallIcon) ? "\\Game\\FactoryGame\\Buildable\\Building\\Wall\\UI\\wall-conveyor-perpendicular-steel_256.png" : null)));
		if (1 == 0)
		{
		}
		return result;
	}

	public static string? ReplaceBigIconPathFromClass(Class2 class2)
	{
		string className = class2.ClassName;
		if (1 == 0)
		{
		}
		string result = ((className == "Desc_Wall_Conveyor_8x4_04_C") ? (IsIconEmpty(class2.mPersistentBigIcon) ? "\\Game\\FactoryGame\\Buildable\\Building\\Wall\\UI\\Wall_Conveyor_Perpendicular_Orange_512.png" : null) : ((!(className == "Desc_Wall_Conveyor_8x4_04_Steel_C")) ? class2.mPersistentBigIcon : (IsIconEmpty(class2.mPersistentBigIcon) ? "\\Game\\FactoryGame\\Buildable\\Building\\Wall\\UI\\wall-conveyor-perpendicular-steel_256.png" : null)));
		if (1 == 0)
		{
		}
		return result;
	}

	private static bool IsIconEmpty(string icon)
	{
		if (!string.IsNullOrEmpty(icon) && !icon.Contains("01_White") && !(icon == "None"))
		{
			throw new ArgumentException("Icon not empty: " + icon);
		}
		return true;
	}
}
