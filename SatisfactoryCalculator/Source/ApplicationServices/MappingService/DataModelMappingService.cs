using System.Diagnostics.Eventing.Reader;
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
            Buildings = buildingDictionary.Values.ToArray(),
            Generators = generators,
            Recipes = recipes,
            ReferenceDictionary = referenceDictionary,
            LastSyncDate = lastSyncDate
        };
    }

    #region Model Methods

    private IItem[] MapToItemModels(List<Item> items) => items.Select(p => new Models.Item
    {
        ClassName = p.ClassName,
        Name = p.DisplayName,
        Description = p.Description,
        Form = p.Form,
        EnergyValue = p.EnergyValue,
        Image = BitmapImageCache.Fetch(SelectImagePath(p.SmallIconPath, p.BigIconPath))
    }).Cast<IItem>().OrderBy(p => p.Name).ToArray();

    private IBuilding[] MapToBuildingModels(List<Building> buildings)
    {
        return buildings.Select(p => new Models.Building
        {
            ClassName = p.ClassName,
            Name = p.DisplayName,
            Description = p.Description,
            Form = p.Form,
            Image = BitmapImageCache.Fetch(SelectImagePath(p.SmallIconPath, p.BigIconPath)),
            ManufactoringSpeed = p.ManuFacturingSpeed,
            PowerConsumption = p.PowerConsumption,
            PowerConsumptionRange = p.PowerConsumptionRange
        }).Cast<IBuilding>().OrderBy(p => p.Name).ToArray();
    }

    private IGenerator[] MapToGeneratorModels(List<DocsServices.Models.DataModels.Generator> generators, Dictionary<string, IBuilding> buildingDictionary) => generators
            .Select(p => MapToGeneratorModel(p, buildingDictionary))
            .OrderBy(p => p.Name)
            .ToArray();

    private IGenerator MapToGeneratorModel(DocsServices.Models.DataModels.Generator generator, Dictionary<string, IBuilding> buildingDictionary)
    {
        var buildingReference = buildingDictionary[generator.ClassName];
        var mappedGenerator = new Generator
        {
            ClassName = generator.ClassName,
            Name = buildingReference.Name,
            Description = buildingReference.Description,
            Image = buildingReference.Image,
            SupplementalToPowerRatio = generator.SupplementToPowerRatio,
            SupplementalLoadAmount = generator.SupplementalLoadAmount,
            PowerProduction = generator.PowerProduction,
        };
        buildingDictionary[generator.ClassName] = mappedGenerator;
        return mappedGenerator;
    }

    private Fuel[] MapToFuelModels(List<DocsServices.Models.DataModels.Generator> generators, IGenerator[] generatorModels, Dictionary<string, IItem> itemDictionary)
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

    private Fuel MapToFuelModel(DocsServices.Models.DataModels.Fuel fuel, IGenerator generator, Dictionary<string, IItem> itemDictionary)
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

        return new()
        {
            Ingredient = ingredient,
            SupplementalIngredient = supplementalIngredient,
            ByProduct = byProductItem,
            ByProductAmount = byProductAmount,
            Generator = generator
        };
    }

    private FuelItem MapToFuelContentModel(IItem item, double amount) => new FuelItem
    {
        Item = item,
        AmountPerMinute = amount,
    };

    private IRecipe[] MapToRecipeModels(List<Recipe> recipes, Dictionary<string, IItem> itemDictionary, Dictionary<string, IBuilding> buildingDictionary) =>
        recipes
            .Select(p => MapToRecipeModel(p, itemDictionary, buildingDictionary))
            .OrderRecipeModel();

    private IRecipe MapToRecipeModel(Recipe recipe, Dictionary<string, IItem> itemDictionary, Dictionary<string, IBuilding> buildingDictionary)
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

    private RecipePart MapToRecipeContentModel(Reference recipeItem, double manufactoringDuration, Dictionary<string, IItem> itemDictionary, Dictionary<string, IBuilding> buildingDictionary, bool setOnlyAmount)
    {
        IEntity? recipePart = null;
        Form? form = null;
        if(itemDictionary.TryGetValue(recipeItem.ClassName, out var item))
        {
            recipePart = item;
            form = item.Form;
        }
        else
        {
            recipePart = buildingDictionary[recipeItem.ClassName];
        }

        return new(part: recipePart,
            amount: _calculationService.NormalizeAmount(form, recipeItem.Amount), sourceAmount: recipeItem.Amount,
            amountPerMinute: setOnlyAmount
                ? null
                : _calculationService.CalculateAmountPerMinte(form, recipeItem.Amount, manufactoringDuration));
    }

    private RecipeBuilding MapToRecipeBuildingModel(IBuilding building, PowerConsumptionRange? powerConsumptionRange) => new RecipeBuilding
    {
        Building = building,
        PowerConsumptionRange = powerConsumptionRange
    };

    #endregion

    #region Dictionary Methods

    private Dictionary<string, IItem> MapToItemDictionary(IItem[] items) =>
        items.ToDictionary(p => p.ClassName, p => p);

    private Dictionary<string, IBuilding> MapToBuildingDictionary(IBuilding[] buildings) =>
        buildings.ToDictionary(p => p.ClassName, p => p as IBuilding);

    private Dictionary<string, EntityReference> MapToEntityReferenceDictionary(Dictionary<string, IItem> itemDictionary, Dictionary<string, IBuilding> buildingDictionary, Fuel[] fuels, IRecipe[] recipes)
    {
        var entityReferences = new Dictionary<string, EntityReference>();

        foreach (var item in itemDictionary.Values)
            entityReferences.Add(item.ClassName, CreateEntityReference(item.ClassName, buildingDictionary, fuels, recipes));

        foreach (var building in buildingDictionary.Values)
            entityReferences.Add(building.ClassName, CreateEntityReference(building.ClassName, buildingDictionary, fuels, recipes));

        return entityReferences;
    }

    private EntityReference CreateEntityReference(string entityClassName, Dictionary<string, IBuilding> buildingDictionary, Fuel[] fuels, IRecipe[] recipes) =>
        new(entityClassName: entityClassName,
            recipeIngredient: GetRecipeIngredientReferences(entityClassName, recipes, buildingDictionary),
            recipeBuildingIngredient: GetRecipeBuildingIngredientReferences(entityClassName, recipes, buildingDictionary), 
            recipeProduct: GetRecipeProductReferences(entityClassName, recipes),
            recipeBuilding: GetRecipeBuildingReferences(entityClassName, recipes),
            fuelIngredient: GetFuelIngredientReferences(entityClassName, fuels),
            fuelByProduct: GetFuelByProductReferences(entityClassName, fuels),
            fuelGenerator: GetFuelGeneratorReferences(entityClassName, fuels));

    private IRecipe[] GetRecipeIngredientReferences(string entityClassName, IRecipe[] recipes, Dictionary<string, IBuilding> buildingDictionary) => recipes
        .Where(recipe => recipe.Ingredients.Any(p => p.Part.ClassName == entityClassName) &&
                            recipe.Products.All(p => !buildingDictionary.ContainsKey(p.Part.ClassName)))
        .ToArray();

    private IRecipe[] GetRecipeBuildingIngredientReferences(string entityClassName, IRecipe[] recipes, Dictionary<string, IBuilding> buildingDictionary) => recipes
        .Where(recipe => recipe.Ingredients.Any(p => p.Part.ClassName == entityClassName) &&
                            recipe.Products.Any(p => buildingDictionary.ContainsKey(p.Part.ClassName)))
        .ToArray();

    private IRecipe[] GetRecipeProductReferences(string entityClassName, IRecipe[] recipes) => recipes
        .Where(recipe => recipe.Products.Any(p => p.Part.ClassName == entityClassName))
        .ToArray();

    private IRecipe[] GetRecipeBuildingReferences(string entityClassName, IRecipe[] recipes) => recipes
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