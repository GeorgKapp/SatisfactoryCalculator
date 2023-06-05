using Building = SatisfactoryCalculator.DocsServices.Models.DataModels.Building;
using Consumable = SatisfactoryCalculator.DocsServices.Models.DataModels.Consumable;
using Equipment = SatisfactoryCalculator.DocsServices.Models.DataModels.Equipment;
using Fuel = SatisfactoryCalculator.Source.Models.Fuel;
using Generator = SatisfactoryCalculator.DocsServices.Models.DataModels.Generator;
using Item = SatisfactoryCalculator.DocsServices.Models.DataModels.Item;
using Recipe = SatisfactoryCalculator.DocsServices.Models.DataModels.Recipe;

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
        
        progress?.ReportOrThrow("Map Equipments", token);
        var equipments = MapToEquipments(data.Equipments, itemDictionary);
        
        progress?.ReportOrThrow("Map Consumables", token);
        var consumables = MapToConsumables(data.Consumables, itemDictionary);

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
            Items = itemDictionary.Values.ToArray(),
            Equipments = equipments,
            Consumables = consumables,
            Buildings = buildingDictionary.Values.ToArray(),
            Generators = generators,
            Recipes = recipes,
            ReferenceDictionary = referenceDictionary,
            LastSyncDate = lastSyncDate
        };
    }

    #region Model Methods

    private IItem[] MapToItemModels(IEnumerable<Item> items) => items.Select(p => new Models.Item(p.ClassName,
        p.DisplayName, p.Description, p.Form, p.EnergyValue,
        BitmapImageCache.Fetch(SelectImagePath(p.SmallIconPath, p.BigIconPath)))).Cast<IItem>().OrderBy(p => p.Name).ToArray();
    
    // ReSharper disable once HeapView.ClosureAllocation
    private IEquipment[] MapToEquipments(IEnumerable<Equipment> equipments, IDictionary<string, IItem> itemDictionary) => equipments
        .Select(p => MapToEquipment(p, itemDictionary))
        .OrderBy(p => p.Name)
        .ToArray();

    private IEquipment MapToEquipment(Equipment equipment, IDictionary<string, IItem> itemDictionary)
    {
        var item = itemDictionary[equipment.ClassName];
        var mappedEquipment = new Models.Equipment(item.ClassName, item.Name, item.Image, item.Description, item.Form, item.EnergyValue, equipment.EquipmentSlot);
        itemDictionary[equipment.ClassName] = mappedEquipment;
        return mappedEquipment;
    }
    
    private IConsumable[] MapToConsumables(List<Consumable> consumables, Dictionary<string, IItem> itemDictionary) => consumables
        .Select(p => MapToConsumable(p, itemDictionary))
        .OrderBy(p => p.Name)
        .ToArray();

    private IConsumable MapToConsumable(Consumable consumable, Dictionary<string, IItem> itemDictionary)
    {
        var item = itemDictionary[consumable.ClassName];
        var mappedConsumable = new Models.Consumable()
        {
            ClassName = item.ClassName,
            Name = item.Name,
            Description = item.Description,
            Form = item.Form,
            EnergyValue = item.EnergyValue,
            Image = item.Image,
            HealthGain = consumable.HealthGain
        };
        itemDictionary[consumable.ClassName] = mappedConsumable;
        return mappedConsumable;
    }

    private IBuilding[] MapToBuildingModels(List<Building> buildings)
    {
        return buildings.Select(p => new Models.Building
        {
            ClassName = p.ClassName,
            Name = p.DisplayName,
            Description = p.Description,
            Image = BitmapImageCache.Fetch(SelectImagePath(p.SmallIconPath, p.BigIconPath)),
            ManufactoringSpeed = p.ManuFacturingSpeed,
            PowerConsumption = p.PowerConsumption,
            PowerConsumptionRange = p.PowerConsumptionRange
        }).Cast<IBuilding>().OrderBy(p => p.Name).ToArray();
    }

    // ReSharper disable once HeapView.ClosureAllocation
    private IGenerator[] MapToGeneratorModels(IEnumerable<Generator> generators, IDictionary<string, IBuilding> buildingDictionary) => generators
            .Select(p => MapToGeneratorModel(p, buildingDictionary))
            .OrderBy(p => p.Name)
            .ToArray();

    private static IGenerator MapToGeneratorModel(Generator generator, IDictionary<string, IBuilding> buildingDictionary)
    {
        var buildingReference = buildingDictionary[generator.ClassName];
        var mappedGenerator = new Models.Generator
        {
            ClassName = generator.ClassName,
            Name = buildingReference.Name,
            Description = buildingReference.Description,
            Image = buildingReference.Image,
            SupplementalToPowerRatio = generator.SupplementToPowerRatio,
            SupplementalLoadAmount = generator.SupplementalLoadAmount,
            PowerProduction = generator.PowerProduction
        };
        buildingDictionary[generator.ClassName] = mappedGenerator;
        return mappedGenerator;
    }

    private Fuel[] MapToFuelModels(IEnumerable<Generator> generators, IGenerator[] generatorModels, IDictionary<string, IItem> itemDictionary)
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

    private Fuel MapToFuelModel(DocsServices.Models.DataModels.Fuel fuel, IGenerator generator, IDictionary<string, IItem> itemDictionary)
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

        return new(ingredient: ingredient, supplementalIngredient: supplementalIngredient, byProduct: byProductItem,
            byProductAmount: byProductAmount, generator: generator);
    }

    private FuelItem MapToFuelContentModel(IItem item, double amount) => new(item, amount);

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
            recipe.ClassName, 
            recipe.DisplayName,
            recipe.IsAlternate, recipe.ConstructedByBuildGun,
            recipe.ConstructedInWorkbench, recipe.ConstructedInWorkshop,
            recipe.ManufactoringDuration, ingredients, products,
            buildings.ToArray());
    }

    private RecipePart MapToRecipeContentModel(Reference recipeItem, double manufactoringDuration, Dictionary<string, IItem> itemDictionary, Dictionary<string, IBuilding> buildingDictionary, bool setOnlyAmount)
    {
        IEntity? recipePart;
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

        return new(recipePart,
            _calculationService.NormalizeAmount(form, recipeItem.Amount), recipeItem.Amount,
            setOnlyAmount
                ? null
                : _calculationService.CalculateAmountPerMinte(form, recipeItem.Amount, manufactoringDuration));
    }

    private RecipeBuilding MapToRecipeBuildingModel(IBuilding building, PowerConsumptionRange? powerConsumptionRange) =>
        new(building, powerConsumptionRange);

    #endregion

    #region Dictionary Methods

    private Dictionary<string, IItem> MapToItemDictionary(IItem[] items) =>
        items.ToDictionary(p => p.ClassName, p => p);

    private Dictionary<string, IBuilding> MapToBuildingDictionary(IBuilding[] buildings) =>
        buildings.ToDictionary(p => p.ClassName, p => p);

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
        new(entityClassName,
            GetRecipeIngredientReferences(entityClassName, recipes, buildingDictionary),
            GetRecipeBuildingIngredientReferences(entityClassName, recipes, buildingDictionary), 
            GetRecipeProductReferences(entityClassName, recipes),
            GetRecipeBuildingReferences(entityClassName, recipes),
            GetFuelIngredientReferences(entityClassName, fuels),
            GetFuelByProductReferences(entityClassName, fuels),
            GetFuelGeneratorReferences(entityClassName, fuels));

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