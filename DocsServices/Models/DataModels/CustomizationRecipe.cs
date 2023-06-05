#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class CustomizationRecipe : IRecipe
{
	public string ClassName { get; set; }
	public string DisplayName { get; set; }
	public Reference[] Ingredients { get; set; }
	public Reference[] Products { get; set; }
	public string[] Buildings { get; set; }
	public double ManualManufacturingMultiplier { get; set; }
	public double ManufactoringDuration { get; set; }
	public double? ManufacturingMenuPriority { get; set; }
	public bool ConstructedByBuildGun { get; set; }
	public bool ConstructedInWorkshop { get; set; }
	public bool ConstructedInWorkbench { get; set; }
	public bool IsSwatchRecipe { get; set; }
	public bool IsPatternRemover { get; set; }
	public PowerConsumptionRange? VariablePowerConsumptionRange { get; set; }
}
