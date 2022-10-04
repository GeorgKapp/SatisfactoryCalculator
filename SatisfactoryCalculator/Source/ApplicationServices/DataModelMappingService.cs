using Building = SatisfactoryCalculator.DocsServices.Models.DataModels.Building;
using Item = SatisfactoryCalculator.DocsServices.Models.DataModels.Item;
using Recipe = SatisfactoryCalculator.DocsServices.Models.DataModels.Recipe;
using Reference = SatisfactoryCalculator.DocsServices.Models.DataModels.Reference;

namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class DataModelMappingService
{
    public ConfigurationModel MapToConfigurationModel(Data? data, IExtendedProgress<string> progress = null, CancellationToken? token = null)
    {
        if (data is null)
            return new();

        ProgressUtility.ReportOrThrow("Map Items", progress, token);
        var items = MapToItemModels(data.Items);

        ProgressUtility.ReportOrThrow("Map Buildings", progress, token);
        var buildings = MapToBuildingModels(data.Buildings);

        ProgressUtility.ReportOrThrow("Map Recipes", progress, token);
        var recipes = MapToRecipeModels(data.Recipes);

        return new ConfigurationModel
        {
            Items = new ObservableCollection<ItemModel>(items),
            Buildings = new ObservableCollection<BuildingModel>(buildings),
            Recipes = new ObservableCollection<RecipeModel>(recipes),
            LastSyncDate = File.Exists(Constants.InformationFileName) ? new DateTime?(File.GetLastWriteTime(Constants.InformationFileName)) : null
        };
    }

    private List<ItemModel> MapToItemModels(List<Item> items)
    {
        return items.Select(p => new ItemModel
        {
            ClassName = p.ClassName,
            Name = p.DisplayName,
            Description = p.Description,
            ImagePath = SelectImagePath(p.SmallIconPath, p.BigIconPath)
        }).ToList();
    }

    public List<ItemModel> MapToItemModels(List<Building> buildings)
    {
        return buildings.Select(p => new ItemModel
        {
            ClassName = p.ClassName,
            Name = p.DisplayName,
            Description = p.Description,
            ImagePath = SelectImagePath(p.SmallIconPath, p.BigIconPath)
        }).ToList();
    }

    public ItemModel MapToItemModel(Building building)
    {
        return new ItemModel
        {
            ClassName = building.ClassName,
            Name = building.DisplayName,
            Description = building.Description,
            ImagePath = SelectImagePath(building.SmallIconPath, building.BigIconPath)
        };
    }

    private List<BuildingModel> MapToBuildingModels(List<Building> buildings)
    {
        return buildings.Select(p => new BuildingModel
        {
            ClassName = p.ClassName,
            Name = p.DisplayName,
            Description = p.Description,
            ImagePath = SelectImagePath(p.SmallIconPath, p.BigIconPath),
            PowerConsumption = p.PowerConsumption,
            PowerConsumptionRange = p.PowerConsumptionRange
        }).ToList();
    }

    private List<RecipeModel> MapToRecipeModels(List<Recipe> recipes)
    {
        return recipes.Select(p => new RecipeModel
        {
            ClassName = p.ClassName,
            RecipeName = p.DisplayName,
            IsAlternateRecipe = p.IsAlternate,
            ConstructedByBuildGun = p.ConstructedByBuildGun,
            ConstructedInWorkbench = p.ConstructedInWorkbench,
            ConstructedInWorkshop = p.ConstructedInWorkshop,
            Ingredients = p.Ingredients.Select(ingredient => MapToRecipeItemModel(ingredient)).ToArray(),
            Products = p.Products.Select(product => MapToRecipeItemModel(product)).ToArray(),
            Buildings = p.Buildings.Select(building => MapToRecipeBuildingModel(building, p.VariablePowerConsumptionRange)).ToArray()
        }).ToList();
    }

    private RecipeContentModel MapToRecipeItemModel(Reference recipeItem)
    {
        return new RecipeContentModel
        {
            ItemName = recipeItem.ClassName,
            StackSize = recipeItem.Amount,
            AmountPerMinute = recipeItem.AmountPerMinute
        };
    }

    private RecipeBuildingModel MapToRecipeBuildingModel(string building, PowerConsumptionRange? powerConsumptionRange)
    {
        return new RecipeBuildingModel
        {
            BuildingName = building,
            PowerConsumptionRange = powerConsumptionRange
        };
    }

    private string SelectImagePath(string smallIconPath, string bigIconPath) =>
        string.IsNullOrEmpty(smallIconPath) 
            ? bigIconPath
            : smallIconPath;
}
