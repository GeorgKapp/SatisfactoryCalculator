namespace SatisfactoryCalculator.Tests.References;

internal static class RecipeModels
{
    public static readonly Recipe CableRecipe = new(
        "Cable_C", 
        "Cable", 
        false, 
        false, 
        true,
        false, 2,
        new[] { new RecipePart(ItemModels.Wire, 2) },
        new[] { new RecipePart(ItemModels.Cable, 1) },
        new[] { new RecipeBuilding(BuildingModels.Constructor) });
    
    public static readonly Recipe IronPlateRecipe = new(
        "IronPlate_C", 
        "Iron Plate", 
        false, 
        false, 
        true,
        false, 6,
        new[] { new RecipePart(ItemModels.IronIngot, 3) },
        new[] { new RecipePart(ItemModels.IronPlate, 2) },
        new[] { new RecipeBuilding(BuildingModels.Constructor) });

    public static readonly Recipe IronRodRecipe = new(
        "IronRod_C", 
        "Iron Rod", 
        false, 
        false, 
        true,
        false, 4,
        new[] { new RecipePart(ItemModels.IronIngot, 1) },
        new[] { new RecipePart(ItemModels.IronRod, 1) },
        new[] { new RecipeBuilding(BuildingModels.Constructor) });
    
    public static readonly Recipe ResidualPlasticRecipe = new(
        "ResidualPlastic_C", 
        "Residual Plastic", 
        false, 
        false, 
        false,
        false, 6,
        new[] { new RecipePart(ItemModels.PolymerResin, 6), 
                         new RecipePart(ItemModels.Water, 2000) },
        new[] { new RecipePart(ItemModels.Plastic, 2) },
        new[] { new RecipeBuilding(BuildingModels.OilRefinery) });
}
