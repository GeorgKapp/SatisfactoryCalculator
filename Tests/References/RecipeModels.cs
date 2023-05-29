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
                ItemName = ItemModels.Wire.ClassName,
                SourceAmount = 2
            },  
        },

        Products = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.Cable,
                ItemName = ItemModels.Cable.ClassName,
                SourceAmount = 1
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
                ItemName = ItemModels.IronIngot.ClassName,
                SourceAmount = 3
            },
        },

        Products = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.IronPlate,
                ItemName = ItemModels.IronPlate.ClassName,
                SourceAmount = 2
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
                ItemName = ItemModels.PolymerResin.ClassName,
                SourceAmount = 6
            },
            new RecipeContentModel
            {
                Item = ItemModels.Water,
                ItemName = ItemModels.Water.ClassName,
                SourceAmount = 2000
            },
        },

        Products = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.Plastic,
                ItemName = ItemModels.Plastic.ClassName,
                SourceAmount = 2
            },
        }
    };
}
