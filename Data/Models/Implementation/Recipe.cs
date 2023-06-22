namespace Data.Models.Implementation;

public class Recipe : IClassNamePrimaryKey, INameEntity
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public decimal ManualManufacturingMultiplier { get; set; }
    public decimal ManufactoringDuration { get; set; }
    public decimal? ManufacturingMenuPriority { get; set; }
    public bool ConstructedByBuildGun { get; set; }
    public bool ConstructedInWorkshop { get; set; }
    public bool ConstructedInWorkbench { get; set; }
    public bool IsAlternate { get; set; }
    public PowerConsumptionRange? VariablePowerConsumptionRange { get; set; }
    public ICollection<Building> Buildings { get; set; } = new List<Building>();
    public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    public ICollection<RecipeProduct> Products { get; set; } = new List<RecipeProduct>();
}
