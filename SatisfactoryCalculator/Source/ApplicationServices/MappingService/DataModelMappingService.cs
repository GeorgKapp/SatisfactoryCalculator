using Ammunition = SatisfactoryCalculator.DocsServices.Models.DataModels.Ammunition;
using Building = SatisfactoryCalculator.DocsServices.Models.DataModels.Building;
using Consumable = SatisfactoryCalculator.DocsServices.Models.DataModels.Consumable;
using Equipment = SatisfactoryCalculator.DocsServices.Models.DataModels.Equipment;
using Fuel = SatisfactoryCalculator.Source.Models.Fuel;
using Generator = SatisfactoryCalculator.DocsServices.Models.DataModels.Generator;
using Item = SatisfactoryCalculator.DocsServices.Models.DataModels.Item;
using Recipe = SatisfactoryCalculator.DocsServices.Models.DataModels.Recipe;
using Weapon = SatisfactoryCalculator.DocsServices.Models.DataModels.Weapon;

// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable HeapView.ClosureAllocation

// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
internal class DataModelMappingService
{
    public DataModelMappingService(CalculationService calculationService)
    {
        _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
    }

    public DataModelMappingResult? MapToConfigurationModel(Data? data, IExtendedProgress<string>? progress = null, CancellationToken? token = null)
    {
        if (data is null)
            return null;
        
        progress?.ReportOrThrow("Map Items", token);
        var items = MapToItemModels(data.Items);
        var itemDictionary = MapToItemDictionary(items);
        
        progress?.ReportOrThrow("Map Equipments", token);
        var equipments = MapToEquipments(data.Equipments, itemDictionary);
        
        progress?.ReportOrThrow("Map Consumables", token);
        var consumables = MapToConsumables(data.Consumables, itemDictionary);
        
        progress?.ReportOrThrow("Map Weapons", token);
        var weapons = MapToWeapons(data.Weapons, itemDictionary);
        
        progress?.ReportOrThrow("Map Ammunitions", token);
        var ammunitions = MapToAmmunitions(data.Ammunition, itemDictionary);

        LinkWeaponsAndAmmunition(data.Weapons, data.Ammunition, itemDictionary);

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
        
        return new(
            itemDictionary.Values.ToArray(), 
            equipments, 
            consumables,
            weapons,
            ammunitions,
            buildingDictionary.Values.ToArray(), 
            generators, 
            recipes,
            referenceDictionary, 
            lastSyncDate);
    }

    #region Model Methods

    private IEnumerable<IItem> MapToItemModels(IEnumerable<Item> items) => items.Select(p => new Models.Item
    { 
        ClassName = p.ClassName,
        Name = p.DisplayName,
        Description = p.Description,
        Form = p.Form,
        EnergyValue = p.EnergyValue,
        Image = BitmapImageCache.Fetch(SelectImagePath(p.SmallIconPath, p.BigIconPath))
    }).OrderBy(p => p.Name).ToArray();
    
    // ReSharper disable once HeapView.ClosureAllocation
    private IEquipment[] MapToEquipments(IEnumerable<Equipment> equipments, IDictionary<string, IItem> itemDictionary) => equipments
        .Select(p => MapToEquipment(p, itemDictionary))
        .OrderBy(p => p.Name)
        .ToArray();

    private IEquipment MapToEquipment(Equipment equipment, IDictionary<string, IItem> itemDictionary)
    {
        var item = itemDictionary[equipment.ClassName];
        
        var mappedEquipment = new Models.Equipment
        {
            ClassName = item.ClassName, 
            Name = item.Name, 
            Description = item.Description, 
            Image = item.Image,
            Form = item.Form, 
            EnergyValue = item.EnergyValue, 
            EquipmentSlot = equipment.EquipmentSlot
        };
        
        itemDictionary[equipment.ClassName] = mappedEquipment;
        return mappedEquipment;
    }
    
    // ReSharper disable once HeapView.ClosureAllocation
    private IConsumable[] MapToConsumables(IEnumerable<Consumable> consumables, IDictionary<string, IItem> itemDictionary) => consumables
        .Select(p => MapToConsumable(p, itemDictionary))
        .OrderBy(p => p.Name)
        .ToArray();

    private IConsumable MapToConsumable(Consumable consumable, IDictionary<string, IItem> itemDictionary)
    {
        var item = itemDictionary[consumable.ClassName];
        
        var mappedConsumable = new Models.Consumable
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
    
    // ReSharper disable once HeapView.ClosureAllocation
    private IWeapon[] MapToWeapons(IEnumerable<Weapon> weapons, IDictionary<string, IItem> itemDictionary) => weapons
        .Select(p => MapToWeapon(p, itemDictionary))
        .OrderBy(p => p.Name)
        .ToArray();

    private IWeapon MapToWeapon(Weapon weapon, IDictionary<string, IItem> itemDictionary)
    {
        var item = itemDictionary[weapon.ClassName];
        
        var mappedWeapon = new Models.Weapon
        {
            ClassName = item.ClassName, 
            Name = item.Name,
            Description = item.Description, 
            Form = item.Form, 
            EnergyValue = item.EnergyValue, 
            Image = item.Image,
            EquipmentSlot = weapon.EquipmentSlot,
            DamageMultiplier = weapon.DamageMultiplier,
            ReloadTime = weapon.ReloadTime,
            AutoReloadDelay = weapon.AutoReloadDelay
        };
        
        itemDictionary[weapon.ClassName] = mappedWeapon;
        return mappedWeapon;
    }
    
    // ReSharper disable once HeapView.ClosureAllocation
    private IAmmunition[] MapToAmmunitions(IEnumerable<Ammunition> ammunitions, IDictionary<string, IItem> itemDictionary) => ammunitions
        .Select(p => MapToAmmunition(p, itemDictionary))
        .OrderBy(p => p.Name)
        .ToArray();

    private IAmmunition MapToAmmunition(Ammunition ammunition, IDictionary<string, IItem> itemDictionary)
    {
        var item = itemDictionary[ammunition.ClassName];
        
        var mappedAmmunition = new Models.Ammunition
        {
            ClassName = item.ClassName, 
            Name = item.Name,
            Description = item.Description, 
            Form = item.Form, 
            EnergyValue = item.EnergyValue, 
            Image = item.Image,
            MaxAmmoEffectiveRange = ammunition.MaxAmmoEffectiveRange,
            WeaponDamageMultiplier = ammunition.WeaponDamageMultiplier,
            FireRate = ammunition.FireRate,
            ReloadTimeMultiplier = ammunition.ReloadTimeMultiplier
        };
        
        itemDictionary[ammunition.ClassName] = mappedAmmunition;
        return mappedAmmunition;
    }
    
    private void LinkWeaponsAndAmmunition(IEnumerable<Weapon> weapons, IEnumerable<Ammunition> ammunitions, IDictionary<string, IItem> itemDictionary)
    {
        foreach (var ammunition in ammunitions)
        {
            var mappedMunition = (IAmmunition)itemDictionary[ammunition.ClassName];
            mappedMunition.UsedInWeapon = (IWeapon)itemDictionary[ammunition.UsedInWeapon];
        }
        
        foreach (var weapon in weapons)
        {
            var mappedWeapon = (IWeapon)itemDictionary[weapon.ClassName];
            mappedWeapon.Ammunitions = weapon.UsesAmmunition.Select(p => (IAmmunition)itemDictionary[p]).ToArray();
        }
    }

    private IEnumerable<IBuilding> MapToBuildingModels(IEnumerable<Building> buildings)
    {
        return buildings.Select(p => 
            new Models.Building
            {
                ClassName = p.ClassName, 
                Name = p.DisplayName,
                Description = p.Description, 
                Image = BitmapImageCache.Fetch(SelectImagePath(p.SmallIconPath, p.BigIconPath)),
                ManufactoringSpeed = p.ManuFacturingSpeed, 
                PowerConsumption = p.PowerConsumption,
                PowerConsumptionExponent = p.PowerConsumptionExponent,
                PowerConsumptionRange = p.PowerConsumptionRange
            }
        ).OrderBy(p => p.Name).ToArray();
    }

    // ReSharper disable once HeapView.ClosureAllocation
    private IGenerator[] MapToGeneratorModels(IEnumerable<Generator> generators, IDictionary<string, IBuilding> buildingDictionary) => generators
            .Select(p => MapToGeneratorModel(p, buildingDictionary))
            .OrderBy(p => p.Name)
            .ToArray();

    private IGenerator MapToGeneratorModel(Generator generator, IDictionary<string, IBuilding> buildingDictionary)
    {
        var buildingReference = buildingDictionary[generator.ClassName];
        
        var mappedGenerator = new Models.Generator
        {
            ClassName = generator.ClassName,
            Name = buildingReference.Name,
            Description = buildingReference.Description,
            Image = buildingReference.Image,
            PowerProduction = generator.PowerProduction,
            SupplementalToPowerRatio = generator.SupplementToPowerRatio,
            SupplementalLoadAmount = generator.SupplementalLoadAmount
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
            fuelModels.AddRange(generator.Fuels.Select(fuel => MapToFuelModel(fuel, generatorModel, itemDictionary)));
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
        
        if (string.IsNullOrEmpty(fuel.ByProduct))
            return new()
            {
                Ingredient = ingredient, 
                SupplementalIngredient = supplementalIngredient, 
                ByProduct = byProductItem,
                ByProductAmount = byProductAmount, 
                Generator = generator
            };
        
        byProductItem = MapToFuelContentModel(itemDictionary[fuel.ByProduct], 0);
        byProductAmount = fuel.ByProductAmount;

        return new()
        {
            Ingredient = ingredient, 
            SupplementalIngredient = supplementalIngredient, 
            ByProduct = byProductItem,
            ByProductAmount = byProductAmount, 
            Generator = generator
        };
    }

    private FuelItem MapToFuelContentModel(IItem item, double amount) => new(item, amount);

    private IRecipe[] MapToRecipeModels(IEnumerable<Recipe> recipes, IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary) =>
        recipes
            .Select(p => MapToRecipeModel(p, itemDictionary, buildingDictionary))
            .OrderRecipeModel();

    private IRecipe MapToRecipeModel(Recipe recipe, IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary)
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
        
        return new Models.Recipe
        {
            ClassName = recipe.ClassName, 
            Name = recipe.DisplayName,
            IsAlternate = recipe.IsAlternate, 
            ConstructedByBuildGun = recipe.ConstructedByBuildGun,
            ConstructedInWorkbench = recipe.ConstructedInWorkbench, 
            ConstructedInWorkshop = recipe.ConstructedInWorkshop,
            ManufactoringDuration = recipe.ManufactoringDuration, 
            Ingredients = ingredients,
            Products = products,
            Buildings = buildings.ToArray()
        };
    }

    private RecipePart MapToRecipeContentModel(Reference recipeItem, double manufactoringDuration, IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary, bool setOnlyAmount)
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

    private IDictionary<string, IItem> MapToItemDictionary(IEnumerable<IItem> items) =>
        items.ToDictionary(p => p.ClassName, p => p);

    private IDictionary<string, IBuilding> MapToBuildingDictionary(IEnumerable<IBuilding> buildings) =>
        buildings.ToDictionary(p => p.ClassName, p => p);

    // ReSharper disable once HeapView.ClosureAllocation
    private IDictionary<string, EntityReference> MapToEntityReferenceDictionary(IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary, Fuel[] fuels, IRecipe[] recipes)
    {
        var entityReferences = itemDictionary.Values
            .ToDictionary(item => item.ClassName, item => CreateEntityReference(item.ClassName, buildingDictionary, fuels, recipes));

        foreach (var building in buildingDictionary.Values)
            entityReferences.Add(building.ClassName, CreateEntityReference(building.ClassName, buildingDictionary, fuels, recipes));

        return entityReferences;
    }

    private EntityReference CreateEntityReference(string entityClassName, IDictionary<string, IBuilding> buildingDictionary, Fuel[] fuels, IRecipe[] recipes) =>
        new()
        {
            RecipeIngredient = GetRecipeIngredientReferences(entityClassName, recipes, buildingDictionary),
            RecipeBuildingIngredient = GetRecipeBuildingIngredientReferences(entityClassName, recipes, buildingDictionary),
            RecipeProduct = GetRecipeProductReferences(entityClassName, recipes),
            RecipeBuilding = GetRecipeBuildingReferences(entityClassName, recipes),
            FuelIngredient = GetFuelIngredientReferences(entityClassName, fuels),
            FuelByProduct = GetFuelByProductReferences(entityClassName, fuels),
            FuelGenerator = GetFuelGeneratorReferences(entityClassName, fuels)
        };
    
    private IRecipe[] GetRecipeIngredientReferences(string entityClassName, IEnumerable<IRecipe> recipes, IDictionary<string, IBuilding> buildingDictionary) => recipes
        .Where(recipe => recipe.Ingredients.Any(p => p.Part.ClassName == entityClassName) &&
                            recipe.Products.All(p => !buildingDictionary.ContainsKey(p.Part.ClassName)))
        .ToArray();
    
    private IRecipe[] GetRecipeBuildingIngredientReferences(string entityClassName, IEnumerable<IRecipe> recipes, IDictionary<string, IBuilding> buildingDictionary) => recipes
        .Where(recipe => recipe.Ingredients.Any(p => p.Part.ClassName == entityClassName) &&
                            recipe.Products.Any(p => buildingDictionary.ContainsKey(p.Part.ClassName)))
        .ToArray();

    private IRecipe[] GetRecipeProductReferences(string entityClassName, IEnumerable<IRecipe> recipes) => recipes
        .Where(recipe => recipe.Products.Any(p => p.Part.ClassName == entityClassName))
        .ToArray();

    private IRecipe[] GetRecipeBuildingReferences(string entityClassName, IEnumerable<IRecipe> recipes) => recipes
        .Where(recipe => recipe.Buildings.Any(p => p.Building.ClassName == entityClassName))
        .ToArray();

    private Fuel[] GetFuelIngredientReferences(string entityClassName, IEnumerable<Fuel> fuels) => fuels
        .Where(fuel => fuel.Ingredient.Item.ClassName == entityClassName || fuel.SupplementalIngredient?.Item.ClassName == entityClassName)
        .ToArray();

    private Fuel[] GetFuelByProductReferences(string entityClassName, IEnumerable<Fuel> fuels) => fuels
       .Where(fuel => fuel.ByProduct is not null && fuel.ByProduct.Item.ClassName == entityClassName)
       .ToArray();

    private Fuel[] GetFuelGeneratorReferences(string entityClassName, IEnumerable<Fuel> fuels) => fuels
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