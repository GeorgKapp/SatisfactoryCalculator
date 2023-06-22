namespace Data.Models.Implementation;

public class CustomizationRecipe : IClassNamePrimaryKey, INameEntity
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public decimal ManualManufacturingMultiplier { get; set; }
    public decimal ManufactoringDuration { get; set; }
    public decimal? ManufacturingMenuPriority { get; set; }
    public bool ConstructedByBuildGun { get; set; }
    public bool ConstructedInWorkshop { get; set; }
    public bool ConstructedInWorkbench { get; set; }
    public bool IsSwatchRecipe { get; set; }
    public bool IsPatternRemover { get; set; }
    
    public ICollection<CustomizationRecipeIngredient> Ingredients { get; set; } = new List<CustomizationRecipeIngredient>();
}
