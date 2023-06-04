using Building = SatisfactoryCalculator.DocsServices.Models.DataModels.Building;
using Fuel = SatisfactoryCalculator.Source.Models.Fuel;
using Generator = SatisfactoryCalculator.Source.Models.Generator;
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
        
        return new()
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

    private Models.Item[] MapToItemModels(List<Item> items) => items.Select(p => new Models.Item
    {
        ClassName = p.ClassName,
        Name = p.DisplayName,
        Description = p.Description,
        Form = p.Form,
        EnergyValue = p.EnergyValue,
        Image = Application.Current.Dispatcher.Invoke(() => BitmapImageCache.Fetch(SelectImagePath(p.SmallIconPath, p.BigIconPath)))
    }).OrderBy(p => p.Name).ToArray();

    private Models.Item MapToItemModel(Models.Building building) => new()
    {
        Name = building.Name,
        ClassName = building.ClassName,
        Description = building.Description,
        Image = Application.Current.Dispatcher.Invoke(() => building.Image)
    };

    private Models.Building[] MapToBuildingModels(List<Building> buildings) => buildings.Select(p => new Models.Building
    {
        ClassName = p.ClassName,
        Name = p.DisplayName,
        Description = p.Description,
        Form = p.Form,
        Image = Application.Current.Dispatcher.Invoke(() => BitmapImageCache.Fetch(SelectImagePath(p.SmallIconPath, p.BigIconPath))),
        ManufactoringSpeed = p.ManuFacturingSpeed,
        PowerConsumption = p.PowerConsumption,
        PowerConsumptionRange = p.PowerConsumptionRange
    }).OrderBy(p => p.Name).ToArray();

    private Generator[] MapToGeneratorModels(List<DocsServices.Models.DataModels.Generator> generators, Dictionary<string, Models.Building> buildingDictionary) => generators
            .Select(p => MapToGeneratorModel(p, buildingDictionary))
            .OrderBy(p => p.Name)
            .ToArray();

    private Generator MapToGeneratorModel(DocsServices.Models.DataModels.Generator generator, Dictionary<string, Models.Building> buildingDictionary)
    {
        var buildingReference = buildingDictionary[generator.ClassName];
        return new Generator
        {
            ClassName = generator.ClassName,
            Name = buildingReference.Name,
            Description = buildingReference.Description,
            Form = buildingReference.Form,
            Image = Application.Current.Dispatcher.Invoke(() => buildingReference.Image),
            SupplementalToPowerRatio = generator.SupplementToPowerRatio,
            SupplementalLoadAmount = generator.SupplementalLoadAmount,
            PowerProduction = generator.PowerProduction
        };
    }

    private Fuel[] MapToFuelModels(List<DocsServices.Models.DataModels.Generator> generators, Generator[] generatorModels, Dictionary<string, Models.Item> itemDictionary)
    {
        List<Fuel> fuelModels = new();
        foreach (var generator in generators)
        {
            var generatorModel = generatorModels.First(p => p.ClassName == generator.ClassName);
            foreach (var fuel in generator.Fuels)
                fuelModels.Add(MapToFuelModel(fuel, generatorModel, itemDictionary));
        }
        return fuelModels.ToArray();
    }

    private Fuel MapToFuelModel(DocsServices.Models.DataModels.Fuel fuel, Generator generator, Dictionary<string, Models.Item> itemDictionary)
    {
        var ingredient = MapToFuelContentModel(itemDictionary[fuel.FuelClass], 0);

        var supplementalIngredient = !string.IsNullOrEmpty(fuel.SupplementalResourceClass)
            ? MapToFuelContentModel(itemDictionary[fuel.SupplementalResourceClass], 0)
            : null;

        FuelItem? byProductItem = null;
        double? byProductAmount = null;
        if (!string.IsNullOrEmpty(fuel.ByProduct))
        {
            byProductItem = MapToFuelContentModel(itemDictionary[fuel.ByProduct], 0);
            byProductAmount = fuel.ByProductAmount;
        }

        return new Fuel
        {
            Ingredient = ingredient,
            SupplementalIngredient = supplementalIngredient,
            ByProduct = byProductItem,
            ByProductAmount = byProductAmount,
            Generator = generator
        };
    }

    private FuelItem MapToFuelContentModel(Models.Item item, double amount) => new FuelItem
    {
        Item = item,
        AmountPerMinute = amount,
    };

    private Models.Recipe[] MapToRecipeModels(List<Recipe> recipes, Dictionary<string, Models.Item> itemDictionary, Dictionary<string, Models.Building> buildingDictionary) =>
        recipes
            .Select(p => MapToRecipeModel(p, itemDictionary, buildingDictionary))
            .OrderRecipeModel();

    private Models.Recipe MapToRecipeModel(Recipe recipe, Dictionary<string, Models.Item> itemDictionary, Dictionary<string, Models.Building> buildingDictionary)
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
        
        return new Models.Recipe(
            className: recipe.ClassName, 
            name: recipe.DisplayName,
            isAlternateRecipe: recipe.IsAlternate, constructedByBuildGun: recipe.ConstructedByBuildGun,
            constructedInWorkbench: recipe.ConstructedInWorkbench, constructedInWorkshop: recipe.ConstructedInWorkshop,
            manufactoringDuration: recipe.ManufactoringDuration, ingredients: ingredients, products: products,
            buildings: buildings.ToArray());
    }

    private Models.RecipeItem MapToRecipeContentModel(Reference recipeItem, double manufactoringDuration, Dictionary<string, Models.Item> itemDictionary, Dictionary<string, Models.Building> buildingDictionary, bool setOnlyAmount)
    {
        var item = itemDictionary.TryGetValue(recipeItem.ClassName, out var value)
            ? value
            : MapToItemModel(buildingDictionary[recipeItem.ClassName]);

        return new Models.RecipeItem(item: item,
            amount: _calculationService.NormalizeAmount(item.Form, recipeItem.Amount), sourceAmount: recipeItem.Amount,
            amountPerMinute: setOnlyAmount
                ? null
                : _calculationService.CalculateAmountPerMinte(item.Form, recipeItem.Amount, manufactoringDuration));
    }

    private RecipeBuilding MapToRecipeBuildingModel(Models.Building building, PowerConsumptionRange? powerConsumptionRange) => new RecipeBuilding
    {
        Building = building,
        PowerConsumptionRange = powerConsumptionRange
    };

    #endregion

    #region Dictionary Methods

    private Dictionary<string, Models.Item> MapToItemDictionary(Models.Item[] items) =>
        items.ToDictionary(p => p.ClassName, p => p);

    private Dictionary<string, Models.Building> MapToBuildingDictionary(Models.Building[] buildings) =>
        buildings.ToDictionary(p => p.ClassName, p => p);

    private Dictionary<string, EntityReference> MapToEntityReferenceDictionary(Dictionary<string, Models.Item> itemDictionary, Dictionary<string, Models.Building> buildingDictionary, Fuel[] fuels, Models.Recipe[] recipes)
    {
        var entityReferences = new Dictionary<string, EntityReference>();

        foreach (var item in itemDictionary.Values)
            entityReferences.Add(item.ClassName, CreateEntityReference(item.ClassName, buildingDictionary, fuels, recipes));

        foreach (var building in buildingDictionary.Values)
            entityReferences.Add(building.ClassName, CreateEntityReference(building.ClassName, buildingDictionary, fuels, recipes));

        return entityReferences;
    }

    private EntityReference CreateEntityReference(string entityClassName, Dictionary<string, Models.Building> buildingDictionary, Fuel[] fuels, Models.Recipe[] recipes) =>
        new EntityReference(entityClassName: entityClassName,
            recipeIngredient: GetRecipeIngredientReferences(entityClassName, recipes, buildingDictionary),
            recipeBuildingIngredient: GetRecipeBuildingIngredientReferences(entityClassName, recipes, buildingDictionary), 
            recipeProduct: GetRecipeProductReferences(entityClassName, recipes),
            recipeBuilding: GetRecipeBuildingReferences(entityClassName, recipes),
            fuelIngredient: GetFuelIngredientReferences(entityClassName, fuels),
            fuelByProduct: GetFuelByProductReferences(entityClassName, fuels),
            fuelGenerator: GetFuelGeneratorReferences(entityClassName, fuels));

    private Models.Recipe[] GetRecipeIngredientReferences(string entityClassName, Models.Recipe[] recipes, Dictionary<string, Models.Building> buildingDictionary) => recipes
        .Where(recipe => recipe.Ingredients.Any(p => p.Item.ClassName == entityClassName) &&
                            recipe.Products.All(p => !buildingDictionary.ContainsKey(p.Item.ClassName)))
        .ToArray();

    private Models.Recipe[] GetRecipeBuildingIngredientReferences(string entityClassName, Models.Recipe[] recipes, Dictionary<string, Models.Building> buildingDictionary) => recipes
        .Where(recipe => recipe.Ingredients.Any(p => p.Item.ClassName == entityClassName) &&
                            recipe.Products.Any(p => buildingDictionary.ContainsKey(p.Item.ClassName)))
        .ToArray();

    private Models.Recipe[] GetRecipeProductReferences(string entityClassName, Models.Recipe[] recipes) => recipes
        .Where(recipe => recipe.Products.Any(p => p.Item.ClassName == entityClassName))
        .ToArray();

    private Models.Recipe[] GetRecipeBuildingReferences(string entityClassName, Models.Recipe[] recipes) => recipes
        .Where(recipe => recipe.Buildings.Any(p => p.Building.ClassName == entityClassName))
        .ToArray();

    private Fuel[] GetFuelIngredientReferences(string entityClassName, Fuel[] fuels) => fuels
        .Where(fuel => fuel.Ingredient.Item.ClassName == entityClassName || fuel.SupplementalIngredient?.Item.ClassName == entityClassName)
        .ToArray();

    private Fuel[] GetFuelByProductReferences(string entityClassName, Fuel[] fuels) => fuels
       .Where(fuel => fuel.ByProduct is not null && fuel.ByProduct.Item.ClassName == entityClassName)
       .ToArray();

    private Fuel[] GetFuelGeneratorReferences(string entityClassName, Fuel[] fuels) => fuels
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