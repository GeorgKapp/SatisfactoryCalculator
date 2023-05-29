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
                Building = BuildingModels.Constructor
            } 
        },

        Ingredients = new[] 
        {
            new RecipeContentModel 
            { 
                Item = ItemModels.Wire,
                SourceAmount = 2
            },  
        },

        Products = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.Cable,
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
        ConstructedInWorkbench = true,
        ConstructedInWorkshop = false,
        IsAlternateRecipe = false,

        Buildings = new[]
        {
            new RecipeBuildingModel
            {
                Building = BuildingModels.Constructor,
            }
        },

        Ingredients = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.IronIngot,
                SourceAmount = 3
            },
        },

        Products = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.IronPlate,
                SourceAmount = 2
            },
        }
    };
    
    public static RecipeModel IronRodRecipe = new RecipeModel
    {
        ClassName = "IronRod_C",
        RecipeName = "Iron Rod",
        ManufactoringDuration = 4,
        ConstructedByBuildGun = false,
        ConstructedInWorkbench = true,
        ConstructedInWorkshop = false,
        IsAlternateRecipe = false,

        Buildings = new[]
        {
            new RecipeBuildingModel
            {
                Building = BuildingModels.Constructor
            }
        },

        Ingredients = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.IronIngot,
                SourceAmount = 1
            },
        },

        Products = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.IronRod,
                SourceAmount = 1
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
            }
        },

        Ingredients = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.PolymerResin,
                SourceAmount = 6
            },
            new RecipeContentModel
            {
                Item = ItemModels.Water,
                SourceAmount = 2000
            },
        },

        Products = new[]
        {
            new RecipeContentModel
            {
                Item = ItemModels.Plastic,
                SourceAmount = 2
            },
        }
    };
}
