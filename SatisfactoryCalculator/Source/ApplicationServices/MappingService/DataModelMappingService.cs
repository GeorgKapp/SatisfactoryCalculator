using Building = SatisfactoryCalculator.DocsServices.Models.DataModels.Building;
using Item = SatisfactoryCalculator.DocsServices.Models.DataModels.Item;
using Recipe = SatisfactoryCalculator.DocsServices.Models.DataModels.Recipe;
using Reference = SatisfactoryCalculator.DocsServices.Models.DataModels.Reference;

namespace SatisfactoryCalculator.Source.ApplicationServices.MappingService;

internal class DataModelMappingService
{
    public DataModelMappingResult MapToConfigurationModel(Data? data, IExtendedProgress<string> progress = null, CancellationToken? token = null)
    {
        if (data is null)
            return new();

        ProgressUtility.ReportOrThrow("Map Items", progress, token);
        var items = MapToItemModels(data.Items);
        var itemDictionary = MapToItemDictionary(items);

        ProgressUtility.ReportOrThrow("Map Buildings", progress, token);
        var buildings = MapToBuildingModels(data.Buildings);
        var buildingDictionary = MapToBuildingDictionary(buildings);

        ProgressUtility.ReportOrThrow("Map Recipes", progress, token);
        var recipes = MapToRecipeModels(data.Recipes, itemDictionary, buildingDictionary);
        var itemRecipeDictionary = MapToItemRecipeModelDictionary(items, recipes, buildingDictionary);
        var buildingRecipeDictionary = MapToItemRecipeModelDictionary(buildings, recipes);

        var lastSyncDate = GetLastSyncDate();

        return new DataModelMappingResult
        {
            ItemDictionary = itemDictionary,
            BuildingDictionary = buildingDictionary,
            ItemRecipesDictionary = itemRecipeDictionary,
            BuildingRecipesDictionary = buildingRecipeDictionary,
            Items = items,
            Buildings = buildings,
            Recipes = recipes,
            LastSyncDate = lastSyncDate
        };
    }

    private ItemModel[] MapToItemModels(List<Item> items) =>
        items.Select(p => new ItemModel
        {
            ClassName = p.ClassName,
            Name = p.DisplayName,
            Description = p.Description,
            Form = p.Form,
            ImagePath = SelectImagePath(p.SmallIconPath, p.BigIconPath)
        }).ToArray();

    private ItemModel MapToItemModel(BuildingModel buildingModel) =>
        new ItemModel
        {
            Name = buildingModel.Name,
            ClassName = buildingModel.ClassName,
            Description = buildingModel.Description,
            ImagePath = buildingModel.ImagePath
        };

    private BuildingModel[] MapToBuildingModels(List<Building> buildings) =>
        buildings.Select(p => new BuildingModel
        {
            ClassName = p.ClassName,
            Name = p.DisplayName,
            Description = p.Description,
            Form = p.Form,
            ImagePath = SelectImagePath(p.SmallIconPath, p.BigIconPath),
            PowerConsumption = p.PowerConsumption,
            PowerConsumptionRange = p.PowerConsumptionRange
        })
        .ToArray();

    private RecipeModel[] MapToRecipeModels(List<Recipe> recipes, Dictionary<string, ItemModel> itemDictionary, Dictionary<string, BuildingModel> buildingDictionary) =>
        recipes
            .Select(p => MapToRecipeModel(p, itemDictionary, buildingDictionary))
            .ToArray();

    private RecipeModel MapToRecipeModel(Recipe recipe, Dictionary<string, ItemModel> itemDictionary, Dictionary<string, BuildingModel> buildingDictionary)
    {
        var buildings = recipe.Buildings
            .Select(building => MapToRecipeBuildingModel(buildingDictionary[building], recipe.VariablePowerConsumptionRange))
            .ToList();

        if (recipe.ConstructedInWorkbench)
            buildings.Add(MapToRecipeBuildingModel(buildingDictionary["WorkBench_C"], recipe.VariablePowerConsumptionRange));

        if (recipe.ConstructedInWorkshop)
            buildings.Add(MapToRecipeBuildingModel(buildingDictionary["Workshop_C"], recipe.VariablePowerConsumptionRange));

        var ingredients = recipe.Ingredients
            .Select(ingredient => MapToRecipeContentModel(ingredient, itemDictionary, buildingDictionary))
            .ToArray();

        var products = recipe.Products
            .Select(product => MapToRecipeContentModel(product, itemDictionary, buildingDictionary))
            .ToArray();

        return new RecipeModel
        {
            ClassName = recipe.ClassName,
            RecipeName = recipe.DisplayName,
            IsAlternateRecipe = recipe.IsAlternate,
            ConstructedByBuildGun = recipe.ConstructedByBuildGun,
            ConstructedInWorkbench = recipe.ConstructedInWorkbench,
            ConstructedInWorkshop = recipe.ConstructedInWorkshop,
            Ingredients = ingredients,
            Products = products,
            Buildings = buildings.ToArray()
        };
    }

    private RecipeContentModel MapToRecipeContentModel(Reference recipeItem, Dictionary<string, ItemModel> itemDictionary, Dictionary<string, BuildingModel> buildingDictionary)
    {
        var item = itemDictionary.ContainsKey(recipeItem.ClassName)
            ? itemDictionary[recipeItem.ClassName]
            : MapToItemModel(buildingDictionary[recipeItem.ClassName]);

        return new RecipeContentModel
        {
            Item = item,
            ItemName = recipeItem.ClassName,
            StackSize = MapAmount(item.Form, recipeItem.Amount),
            AmountPerMinute = MapAmount(item.Form, recipeItem.AmountPerMinute),
        };
    }

    private RecipeBuildingModel MapToRecipeBuildingModel(BuildingModel buildingModel, PowerConsumptionRange? powerConsumptionRange) =>
        new RecipeBuildingModel
        {
            Building = buildingModel,
            BuildingName = buildingModel.ClassName,
            PowerConsumptionRange = powerConsumptionRange
        };

    private double MapAmount(Form? form, double input)
    {
        if (form is not null && (form == Form.Liquid || form == Form.Gas))
            input /= 1000;

        return input;
    }

    private double? MapAmount(Form? form, double? input) =>
        input is not null
            ? MapAmount(form, input.Value)
            : null;

    private string SelectImagePath(string smallIconPath, string bigIconPath) =>
        string.IsNullOrEmpty(smallIconPath)
            ? bigIconPath
            : smallIconPath;
    private Dictionary<string, ItemModel> MapToItemDictionary(ItemModel[] items) =>
        items.ToDictionary(p => p.ClassName, p => p);

    private Dictionary<string, BuildingModel> MapToBuildingDictionary(BuildingModel[] buildings) =>
        buildings.ToDictionary(p => p.ClassName, p => p);

    private Dictionary<string, ItemRecipeModel> MapToItemRecipeModelDictionary(ItemModel[] items, RecipeModel[] recipes, Dictionary<string, BuildingModel> buildingDictionary)
    {
        var dictionary = new Dictionary<string, ItemRecipeModel>();
        foreach (var item in items)
        {
            var itemRecipeModel = new ItemRecipeModel { ItemName = item.ClassName };
            foreach (var recipe in recipes)
            {
                if (recipe.Ingredients.Where(p => p.Item.ClassName == item.ClassName).Any())
                {
                    if (recipe.Products.Where(p => buildingDictionary.ContainsKey(p.Item.ClassName)).Any())
                        itemRecipeModel.ContainedAsBuildingIngredient = itemRecipeModel.ContainedAsBuildingIngredient.Add(recipe);
                    else
                        itemRecipeModel.ContainedAsIngredient = itemRecipeModel.ContainedAsIngredient.Add(recipe);
                }

                if (recipe.Products.Where(p => p.Item.ClassName == item.ClassName).Any())
                    itemRecipeModel.ContainedAsProduct = itemRecipeModel.ContainedAsProduct.Add(recipe);
            }
            dictionary.Add(item.ClassName, itemRecipeModel);
        }
        return dictionary;
    }

    private Dictionary<string, ItemRecipeModel> MapToItemRecipeModelDictionary(BuildingModel[] buildings, RecipeModel[] recipes)
    {
        var dictionary = new Dictionary<string, ItemRecipeModel>();
        foreach (var building in buildings)
        {
            List<RecipeModel> buildingProductRecipes = new();
            var itemRecipeModel = new ItemRecipeModel { ItemName = building.ClassName };

            foreach (var recipe in recipes)
            {
                if (recipe.Products.Where(p => p.ItemName == building.ClassName).Any())
                    itemRecipeModel.ContainedAsProduct = itemRecipeModel.ContainedAsProduct.Add(recipe);
            }

            dictionary.Add(building.ClassName, itemRecipeModel);
        }
        return dictionary;
    }

    private RecipeModel[] OrderRecipeModel(List<RecipeModel> input) => input
            .OrderBy(p => p.IsAlternateRecipe)
            .ThenBy(p => p.RecipeName)
            .ToArray();

    private DateTime? GetLastSyncDate() =>
        File.Exists(Constants.InformationFileName)
        ? new DateTime?(File.GetLastWriteTime(Constants.InformationFileName))
        : null;
}
