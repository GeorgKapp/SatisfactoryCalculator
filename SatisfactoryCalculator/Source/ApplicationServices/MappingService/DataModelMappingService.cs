using Building = SatisfactoryCalculator.DocsServices.Models.DataModels.Building;
using Item = SatisfactoryCalculator.DocsServices.Models.DataModels.Item;
using Recipe = SatisfactoryCalculator.DocsServices.Models.DataModels.Recipe;
using Reference = SatisfactoryCalculator.DocsServices.Models.DataModels.Reference;

// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class DataModelMappingService
{
    public DataModelMappingService(CalculationService calculationService)
    {
        _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
    }

    public DataModelMappingResult MapToConfigurationModel(Data? data, IExtendedProgress<string>? progress = null, CancellationToken? token = null)
    {
        if (data is null)
            return new();
        
        progress?.ReportOrThrow("Map Items", token);
        var items = MapToItemModels(data.Items);
        var itemDictionary = MapToItemDictionary(items);

        progress?.ReportOrThrow("Map Buildings", token);
        var buildings = MapToBuildingModels(data.Buildings);
        var buildingDictionary = MapToBuildingDictionary(buildings);

        progress?.ReportOrThrow("Map Generators", token);
        var generators = MapToGeneratorModels(data.Generators, buildingDictionary);

        progress?.ReportOrThrow("Map Fuels", token);
        var fuels = MapToFuelModels(data.Generators, generators, itemDictionary);

        progress?.ReportOrThrow("Map Recipes", token);
        var recipes = MapToRecipeModels(data.Recipes, itemDictionary, buildingDictionary);

        progress?.ReportOrThrow("Map References", token);
        var referenceDictionary = MapToEntityReferenceDictionary(itemDictionary, buildingDictionary, fuels, recipes);

        var lastSyncDate = GetLastSyncDate();
        return new DataModelMappingResult
        {
            Items = items,
            Buildings = buildings,
            Generators = generators,
            Fuels = fuels,
            Recipes = recipes,
            ReferenceDictionary = referenceDictionary,
            LastSyncDate = lastSyncDate
        };
    }

    #region Model Methods

    private ItemModel[] MapToItemModels(List<Item> items) => items.Select(p => new ItemModel
    {
        ClassName = p.ClassName,
        Name = p.DisplayName,
        Description = p.Description,
        Form = p.Form,
        EnergyValue = p.EnergyValue,
        ImagePath = SelectImagePath(p.SmallIconPath, p.BigIconPath)
    }).OrderBy(p => p.Name).ToArray();

    private ItemModel MapToItemModel(BuildingModel buildingModel) => new ItemModel
    {
        Name = buildingModel.Name,
        ClassName = buildingModel.ClassName,
        Description = buildingModel.Description,
        ImagePath = buildingModel.ImagePath
    };

    private BuildingModel[] MapToBuildingModels(List<Building> buildings) => buildings.Select(p => new BuildingModel
    {
        ClassName = p.ClassName,
        Name = p.DisplayName,
        Description = p.Description,
        Form = p.Form,
        ImagePath = SelectImagePath(p.SmallIconPath, p.BigIconPath),
        ManufactoringSpeed = p.ManuFacturingSpeed,
        PowerConsumption = p.PowerConsumption,
        PowerConsumptionRange = p.PowerConsumptionRange
    }).OrderBy(p => p.Name).ToArray();

    private GeneratorModel[] MapToGeneratorModels(List<Generator> generators, Dictionary<string, BuildingModel> buildingDictionary) => generators
            .Select(p => MapToGeneratorModel(p, buildingDictionary))
            .OrderBy(p => p.Name)
            .ToArray();

    private GeneratorModel MapToGeneratorModel(Generator generator, Dictionary<string, BuildingModel> buildingDictionary)
    {
        var buildingReference = buildingDictionary[generator.ClassName];
        return new GeneratorModel
        {
            ClassName = generator.ClassName,
            Name = buildingReference.Name,
            Description = buildingReference.Description,
            Form = buildingReference.Form,
            ImagePath = buildingReference.ImagePath,
            SupplementalToPowerRatio = generator.SupplementToPowerRatio,
            SupplementalLoadAmount = generator.SupplementalLoadAmount,
            PowerProduction = generator.PowerProduction
        };
    }

    private FuelModel[] MapToFuelModels(List<Generator> generators, GeneratorModel[] generatorModels, Dictionary<string, ItemModel> itemDictionary)
    {
        List<FuelModel> fuelModels = new();
        foreach (var generator in generators)
        {
            var generatorModel = generatorModels.First(p => p.ClassName == generator.ClassName);
            foreach (var fuel in generator.Fuels)
                fuelModels.Add(MapToFuelModel(fuel, generatorModel, itemDictionary));
        }
        return fuelModels.ToArray();
    }

    private FuelModel MapToFuelModel(Fuel fuel, GeneratorModel generatorModel, Dictionary<string, ItemModel> itemDictionary)
    {
        var ingredient = MapToFuelContentModel(itemDictionary[fuel.FuelClass], 0);

        var supplementalIngredient = !string.IsNullOrEmpty(fuel.SupplementalResourceClass)
            ? MapToFuelContentModel(itemDictionary[fuel.SupplementalResourceClass], 0)
            : null;

        FuelContentModel? byProductItem = null;
        double? byProductAmount = null;
        if (!string.IsNullOrEmpty(fuel.ByProduct))
        {
            byProductItem = MapToFuelContentModel(itemDictionary[fuel.ByProduct], 0);
            byProductAmount = fuel.ByProductAmount;
        }

        return new FuelModel
        {
            Ingredient = ingredient,
            SupplementalIngredient = supplementalIngredient,
            ByProduct = byProductItem,
            ByProductAmount = byProductAmount,
            Generator = generatorModel
        };
    }

    private FuelContentModel MapToFuelContentModel(ItemModel item, double amount) => new FuelContentModel
    {
        Item = item,
        AmountPerMinute = amount,
    };

    private RecipeModel[] MapToRecipeModels(List<Recipe> recipes, Dictionary<string, ItemModel> itemDictionary, Dictionary<string, BuildingModel> buildingDictionary) =>
        recipes
            .Select(p => MapToRecipeModel(p, itemDictionary, buildingDictionary))
            .OrderRecipeModel();

    private RecipeModel MapToRecipeModel(Recipe recipe, Dictionary<string, ItemModel> itemDictionary, Dictionary<string, BuildingModel> buildingDictionary)
    {
        var buildings = recipe.Buildings
            .Select(building => MapToRecipeBuildingModel(buildingDictionary[building], recipe.VariablePowerConsumptionRange))
            .ToList();

        if (recipe.ConstructedInWorkbench)
            buildings.Add(MapToRecipeBuildingModel(buildingDictionary["WorkBench_C"], recipe.VariablePowerConsumptionRange));

        if (recipe.ConstructedInWorkshop)
            buildings.Add(MapToRecipeBuildingModel(buildingDictionary["Workshop_C"], recipe.VariablePowerConsumptionRange));

        var productIsBuilding = recipe.Products
            .Any(p => buildingDictionary.ContainsKey(p.ClassName));

        var ingredients = recipe.Ingredients
            .Select(ingredient => MapToRecipeContentModel(ingredient, recipe.ManufactoringDuration, itemDictionary, buildingDictionary, productIsBuilding))
            .ToArray();

        var products = recipe.Products
            .Select(product => MapToRecipeContentModel(product, recipe.ManufactoringDuration, itemDictionary, buildingDictionary, productIsBuilding))
            .ToArray();
        
        return new RecipeModel(
            className: recipe.ClassName, 
            recipeName: recipe.DisplayName,
            isAlternateRecipe: recipe.IsAlternate, constructedByBuildGun: recipe.ConstructedByBuildGun,
            constructedInWorkbench: recipe.ConstructedInWorkbench, constructedInWorkshop: recipe.ConstructedInWorkshop,
            manufactoringDuration: recipe.ManufactoringDuration, ingredients: ingredients, products: products,
            buildings: buildings.ToArray());
    }

    private RecipeContentModel MapToRecipeContentModel(Reference recipeItem, double manufactoringDuration, Dictionary<string, ItemModel> itemDictionary, Dictionary<string, BuildingModel> buildingDictionary, bool setOnlyAmount)
    {
        var item = itemDictionary.TryGetValue(recipeItem.ClassName, out var value)
            ? value
            : MapToItemModel(buildingDictionary[recipeItem.ClassName]);

        return new RecipeContentModel(item: item,
            amount: _calculationService.NormalizeAmount(item.Form, recipeItem.Amount), sourceAmount: recipeItem.Amount,
            amountPerMinute: setOnlyAmount
                ? null
                : _calculationService.CalculateAmountPerMinte(item.Form, recipeItem.Amount, manufactoringDuration));
    }

    private RecipeBuildingModel MapToRecipeBuildingModel(BuildingModel buildingModel, PowerConsumptionRange? powerConsumptionRange) => new RecipeBuildingModel
    {
        Building = buildingModel,
        PowerConsumptionRange = powerConsumptionRange
    };

    #endregion

    #region Dictionary Methods

    private Dictionary<string, ItemModel> MapToItemDictionary(ItemModel[] items) =>
        items.ToDictionary(p => p.ClassName, p => p);

    private Dictionary<string, BuildingModel> MapToBuildingDictionary(BuildingModel[] buildings) =>
        buildings.ToDictionary(p => p.ClassName, p => p);

    private Dictionary<string, EntityReference> MapToEntityReferenceDictionary(Dictionary<string, ItemModel> itemDictionary, Dictionary<string, BuildingModel> buildingDictionary, FuelModel[] fuels, RecipeModel[] recipes)
    {
        var entityReferences = new Dictionary<string, EntityReference>();

        foreach (var item in itemDictionary.Values)
            entityReferences.Add(item.ClassName, CreateEntityReference(item.ClassName, buildingDictionary, fuels, recipes));

        foreach (var building in buildingDictionary.Values)
            entityReferences.Add(building.ClassName, CreateEntityReference(building.ClassName, buildingDictionary, fuels, recipes));

        return entityReferences;
    }

    private EntityReference CreateEntityReference(string entityClassName, Dictionary<string, BuildingModel> buildingDictionary, FuelModel[] fuels, RecipeModel[] recipes) =>
        new EntityReference(entityClassName: entityClassName,
            recipeIngredient: GetRecipeIngredientReferences(entityClassName, recipes, buildingDictionary),
            recipeBuildingIngredient: GetRecipeBuildingIngredientReferences(entityClassName, recipes, buildingDictionary), 
            recipeProduct: GetRecipeProductReferences(entityClassName, recipes),
            recipeBuilding: GetRecipeBuildingReferences(entityClassName, recipes),
            fuelIngredient: GetFuelIngredientReferences(entityClassName, fuels),
            fuelByProduct: GetFuelByProductReferences(entityClassName, fuels),
            fuelGenerator: GetFuelGeneratorReferences(entityClassName, fuels));

    private RecipeModel[] GetRecipeIngredientReferences(string entityClassName, RecipeModel[] recipes, Dictionary<string, BuildingModel> buildingDictionary) => recipes
        .Where(recipe => recipe.Ingredients.Any(p => p.Item.ClassName == entityClassName) &&
                            recipe.Products.All(p => !buildingDictionary.ContainsKey(p.Item.ClassName)))
        .ToArray();

    private RecipeModel[] GetRecipeBuildingIngredientReferences(string entityClassName, RecipeModel[] recipes, Dictionary<string, BuildingModel> buildingDictionary) => recipes
        .Where(recipe => recipe.Ingredients.Any(p => p.Item.ClassName == entityClassName) &&
                            recipe.Products.Any(p => buildingDictionary.ContainsKey(p.Item.ClassName)))
        .ToArray();

    private RecipeModel[] GetRecipeProductReferences(string entityClassName, RecipeModel[] recipes) => recipes
        .Where(recipe => recipe.Products.Any(p => p.Item.ClassName == entityClassName))
        .ToArray();

    private RecipeModel[] GetRecipeBuildingReferences(string entityClassName, RecipeModel[] recipes) => recipes
        .Where(recipe => recipe.Buildings.Any(p => p.Building.ClassName == entityClassName))
        .ToArray();

    private FuelModel[] GetFuelIngredientReferences(string entityClassName, FuelModel[] fuels) => fuels
        .Where(fuel => fuel.Ingredient.Item.ClassName == entityClassName || fuel.SupplementalIngredient?.Item.ClassName == entityClassName)
        .ToArray();

    private FuelModel[] GetFuelByProductReferences(string entityClassName, FuelModel[] fuels) => fuels
       .Where(fuel => fuel.ByProduct is not null && fuel.ByProduct.Item.ClassName == entityClassName)
       .ToArray();

    private FuelModel[] GetFuelGeneratorReferences(string entityClassName, FuelModel[] fuels) => fuels
       .Where(fuel => fuel.Generator.ClassName == entityClassName)
       .ToArray();

    #endregion

    #region Utility Methods

    private string SelectImagePath(string smallIconPath, string bigIconPath) => string.IsNullOrEmpty(smallIconPath)
        ? bigIconPath
        : smallIconPath;

    private DateTime? GetLastSyncDate() => File.Exists(Constants.InformationFileName)
        ? new DateTime?(File.GetLastWriteTime(Constants.InformationFileName))
        : null;

    #endregion

    private readonly CalculationService _calculationService;
}