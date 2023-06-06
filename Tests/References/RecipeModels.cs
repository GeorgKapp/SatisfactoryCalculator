namespace SatisfactoryCalculator.Tests.References;

internal static class RecipeModels
{
    public static readonly Recipe CableRecipe = new()
    {
       ClassName = "Cable_C", 
       Name = "Cable", 
       ConstructedInWorkbench = true,
       ManufactoringDuration = 2,
       Ingredients = new[] { new RecipePart(ItemModels.Wire, 2) },
       Products = new[] { new RecipePart(ItemModels.Cable, 1) },
       Buildings = new[] { new RecipeBuilding(BuildingModels.Constructor) }
    };
    
    public static readonly Recipe IronPlateRecipe = new()
    {
        ClassName = "IronPlate_C", 
        Name = "Iron Plate", 
        ConstructedInWorkbench = true,
        ManufactoringDuration = 6,
        Ingredients = new[] { new RecipePart(ItemModels.IronIngot, 3) },
        Products = new[] { new RecipePart(ItemModels.IronPlate, 2) },
        Buildings = new[] { new RecipeBuilding(BuildingModels.Constructor) }
    };
    
    public static readonly Recipe IronRodRecipe = new()
    {
        ClassName = "IronRod_C", 
        Name = "Iron Rod", 
        ConstructedInWorkbench = true,
        ManufactoringDuration = 4,
        Ingredients = new[] { new RecipePart(ItemModels.IronIngot, 1) },
        Products = new[] { new RecipePart(ItemModels.IronRod, 1) },
        Buildings = new[] { new RecipeBuilding(BuildingModels.Constructor) }
    };
    
    public static readonly Recipe ResidualPlasticRecipe = new()
    {
        ClassName = "ResidualPlastic_C", 
        Name = "Residual Plastic",
        ManufactoringDuration = 6,
        Ingredients = new[] { new RecipePart(ItemModels.PolymerResin, 6), 
                              new RecipePart(ItemModels.Water, 2000) },
        Products = new[] { new RecipePart(ItemModels.Plastic, 2) },
        Buildings = new[] { new RecipeBuilding(BuildingModels.OilRefinery) }
    };
}
