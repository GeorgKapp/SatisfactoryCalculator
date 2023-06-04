namespace SatisfactoryCalculator.Tests.References;

internal class RecipeModels
{
    public static RecipeModel CableRecipe = new RecipeModel(className: "Cable_C", recipeName: "Cable",
        manufactoringDuration: 2, constructedByBuildGun: false, constructedInWorkbench: false,
        constructedInWorkshop: false, isAlternateRecipe: false, buildings: new[]
        {
            new RecipeBuildingModel
            {
                Building = BuildingModels.Constructor
            }
        }, ingredients: new[]
        {
            new RecipeContentModel(item: ItemModels.Wire, sourceAmount: 2),
        }, products: new[]
        {
            new RecipeContentModel(item: ItemModels.Cable, sourceAmount: 1),
        });

    public static RecipeModel IronPlateRecipe = new RecipeModel(className: "IronPlate_C", recipeName: "Iron Plate",
        manufactoringDuration: 6, constructedByBuildGun: false, constructedInWorkbench: true,
        constructedInWorkshop: false, isAlternateRecipe: false, buildings: new[]
        {
            new RecipeBuildingModel
            {
                Building = BuildingModels.Constructor,
            }
        }, ingredients: new[]
        {
            new RecipeContentModel(item: ItemModels.IronIngot, sourceAmount: 3),
        }, products: new[]
        {
            new RecipeContentModel(item: ItemModels.IronPlate, sourceAmount: 2),
        });
    
    public static RecipeModel IronRodRecipe = new RecipeModel(className: "IronRod_C", recipeName: "Iron Rod",
        manufactoringDuration: 4, constructedByBuildGun: false, constructedInWorkbench: true,
        constructedInWorkshop: false, isAlternateRecipe: false, buildings: new[]
        {
            new RecipeBuildingModel
            {
                Building = BuildingModels.Constructor
            }
        }, ingredients: new[]
        {
            new RecipeContentModel(item: ItemModels.IronIngot, sourceAmount: 1),
        }, products: new[]
        {
            new RecipeContentModel(item: ItemModels.IronRod, sourceAmount: 1),
        });

    public static RecipeModel ResidualPlasticRecipe = new RecipeModel(className: "ResidualPlastic_C",
        recipeName: "Residual Plastic", manufactoringDuration: 6, constructedByBuildGun: false,
        constructedInWorkbench: false, constructedInWorkshop: false, isAlternateRecipe: false, buildings: new[]
        {
            new RecipeBuildingModel
            {
                Building = BuildingModels.OilRefinery,
            }
        }, ingredients: new[]
        {
            new RecipeContentModel(item: ItemModels.PolymerResin, sourceAmount: 6),
            new RecipeContentModel(item: ItemModels.Water, sourceAmount: 2000),
        }, products: new[]
        {
            new RecipeContentModel(item: ItemModels.Plastic, sourceAmount: 2),
        });
}
