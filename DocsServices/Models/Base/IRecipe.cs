namespace SatisfactoryCalculator.DocsServices.Models.Base;

public interface IRecipe : IBase
{
	string DisplayName { get; set; }
	Reference[] Ingredients { get; set; }
	Reference[] Products { get; set; }
	string[] Buildings { get; set; }
	PowerConsumptionRange? VariablePowerConsumptionRange { get; set; }
	double ManualManufacturingMultiplier { get; set; }
	double ManufactoringDuration { get; set; }
	double? ManufacturingMenuPriority { get; set; }
	bool ConstructedByBuildGun { get; set; }
	bool ConstructedInWorkshop { get; set; }
	bool ConstructedInWorkbench { get; set; }
}
