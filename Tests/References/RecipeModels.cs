namespace SatisfactoryCalculator.Tests.References;

internal class RecipeModels
{
    public static RecipeModel CableRecipe = new RecipeModel
    {
        ClassName = "Cable_C",
        RecipeName = "Cable",
        ManufactoringDuration = 2,
        ConstructedByBuildGun = false,
        ConstructedInWorkbench = false,
        ConstructedInWorkshop = false,
        IsAlternateRecipe = false,

        Buildings = new[] 
        { 
            new RecipeBuildingModel 
            {
                Building = BuildingModels.Constructor, 
                BuildingName = BuildingModels.Constructor.ClassName 
            } 
        },

        Ingredients = new[] 
        {
            new RecipeContentModel 
            { 
                Item = ItemModels.Wire,
                Amount = 2
            },  
        },

        Products = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.Cable,
                Amount = 1
            },
        }
    };

    public static RecipeModel IronPlateRecipe = new RecipeModel
    {
        ClassName = "IronPlate_C",
        RecipeName = "Iron Plate",
        ManufactoringDuration = 6,
        ConstructedByBuildGun = false,
        ConstructedInWorkbench = false,
        ConstructedInWorkshop = true,
        IsAlternateRecipe = false,

        Buildings = new[]
        {
            new RecipeBuildingModel
            {
                Building = BuildingModels.Constructor,
                BuildingName = BuildingModels.Constructor.ClassName
            }
        },

        Ingredients = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.IronIngot,
                Amount = 3
            },
        },

        Products = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.IronPlate,
                Amount = 2
            },
        }
    };

    public static RecipeModel ResidualPlasticRecipe = new RecipeModel
    {
        ClassName = "ResidualPlastic_C",
        RecipeName = "Residual Plastic",
        ManufactoringDuration = 6,
        ConstructedByBuildGun = false,
        ConstructedInWorkbench = false,
        ConstructedInWorkshop = false,
        IsAlternateRecipe = false,

        Buildings = new[]
        {
            new RecipeBuildingModel
            {
                Building = BuildingModels.OilRefinery,
                BuildingName = BuildingModels.OilRefinery.ClassName
            }
        },

        Ingredients = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.PolymerResin,
                Amount = 6
            },
            new RecipeContentModel
            {
                Item = ItemModels.Water,
                Amount = 2000
            },
        },

        Products = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.Plastic,
                Amount = 2
            },
        }
    };
}
