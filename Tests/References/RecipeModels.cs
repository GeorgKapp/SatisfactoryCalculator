namespace SatisfactoryCalculator.Tests.References;

internal class RecipeModels
{
    public static Recipe CableRecipe = new Recipe(className: "Cable_C", name: "Cable",
        manufactoringDuration: 2, constructedByBuildGun: false, constructedInWorkbench: false,
        constructedInWorkshop: false, isAlternateRecipe: false, buildings: new[]
        {
            new RecipeBuilding(building: BuildingModels.Constructor)
        }, ingredients: new[]
        {
            new RecipePart(part: ItemModels.Wire, sourceAmount: 2),
        }, products: new[]
        {
            new RecipePart(part: ItemModels.Cable, sourceAmount: 1),
        });

    public static Recipe IronPlateRecipe = new Recipe(className: "IronPlate_C", name: "Iron Plate",
        manufactoringDuration: 6, constructedByBuildGun: false, constructedInWorkbench: true,
        constructedInWorkshop: false, isAlternateRecipe: false, buildings: new[]
        {
            new RecipeBuilding(building: BuildingModels.Constructor)
        }, ingredients: new[]
        {
            new RecipePart(part: ItemModels.IronIngot, sourceAmount: 3),
        }, products: new[]
        {
            new RecipePart(part: ItemModels.IronPlate, sourceAmount: 2),
        });
    
    public static Recipe IronRodRecipe = new Recipe(className: "IronRod_C", name: "Iron Rod",
        manufactoringDuration: 4, constructedByBuildGun: false, constructedInWorkbench: true,
        constructedInWorkshop: false, isAlternateRecipe: false, buildings: new[]
        {
            new RecipeBuilding(building: BuildingModels.Constructor)
        }, ingredients: new[]
        {
            new RecipePart(part: ItemModels.IronIngot, sourceAmount: 1),
        }, products: new[]
        {
            new RecipePart(part: ItemModels.IronRod, sourceAmount: 1),
        });

    public static Recipe ResidualPlasticRecipe = new Recipe(className: "ResidualPlastic_C",
        name: "Residual Plastic", manufactoringDuration: 6, constructedByBuildGun: false,
        constructedInWorkbench: false, constructedInWorkshop: false, isAlternateRecipe: false, buildings: new[]
        {
            new RecipeBuilding(building: BuildingModels.OilRefinery)
        }, ingredients: new[]
        {
            new RecipePart(part: ItemModels.PolymerResin, sourceAmount: 6),
            new RecipePart(part: ItemModels.Water, sourceAmount: 2000),
        }, products: new[]
        {
            new RecipePart(part: ItemModels.Plastic, sourceAmount: 2),
        });
}
