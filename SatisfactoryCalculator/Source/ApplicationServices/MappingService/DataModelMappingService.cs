using Ammunition = Data.Models.Implementation.Ammunition;
using Building = Data.Models.Implementation.Building;
using Consumable = Data.Models.Implementation.Consumable;
using Creature = Data.Models.Implementation.Creature;
using CreatureLoot = Data.Models.Implementation.CreatureLoot;
using Equipment = Data.Models.Implementation.Equipment;
using FuelItem = Data.Models.Implementation.FuelItem;
using Generator = Data.Models.Implementation.Generator;
using Item = Data.Models.Implementation.Item;
using Miner = Data.Models.Implementation.Miner;
using Recipe = Data.Models.Implementation.Recipe;
using Resource = Data.Models.Implementation.Resource;
using Weapon = Data.Models.Implementation.Weapon;

// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable HeapView.ClosureAllocation

// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
internal class DataModelMappingService
{
    public DataModelMappingService(
        ModelCalculationService modelCalculationService,
        IDbContextFactory<ModelContext> modelContextFactory)
    {
        _modelCalculationService = modelCalculationService ?? throw new ArgumentNullException(nameof(modelCalculationService));
        _modelContextFactory = modelContextFactory ?? throw new ArgumentNullException(nameof(modelContextFactory));
    }

    public async Task<DataModelMappingResult> MapConfigurationModelsAsync(IExtendedProgress<string>? progress = null, CancellationToken? token = null)
    {
        await using var modelContext = await _modelContextFactory.CreateDbContextAsync();
        
        progress?.ReportOrThrow("Map Items", token);
        var items = MapToItemModels(modelContext.Items);
        var itemDictionary = MapToItemDictionary(items);
        
        progress?.ReportOrThrow("Map Equipments", token);
        var equipments = MapToEquipments(modelContext.Equipments, itemDictionary);
        
        progress?.ReportOrThrow("Map Consumables", token);
        var consumables = MapToConsumables(modelContext.Consumables, itemDictionary);
        
        progress?.ReportOrThrow("Map Weapons", token);
        var weapons = MapToWeapons(modelContext.Weapons, itemDictionary);
        
        progress?.ReportOrThrow("Map Ammunitions", token);
        var ammunitions = MapToAmmunitions(modelContext.Ammunitions, itemDictionary);
        LinkWeaponsAndAmmunition(modelContext.Weapons, modelContext.Ammunitions, itemDictionary);
        
        progress?.ReportOrThrow("Map Resources", token);
        var resources = MapToResources(modelContext.Resources.LoadAll(), itemDictionary);

        progress?.ReportOrThrow("Map Buildings", token);
        var buildings = MapToBuildingModels(modelContext.Buildings);
        var buildingDictionary = MapToBuildingDictionary(buildings);

        progress?.ReportOrThrow("Map Generators", token);
        var generators = MapToGeneratorModels(modelContext.Generators, buildingDictionary);
        
        progress?.ReportOrThrow("Map Miners", token);
        var miners = MapToMinerModels(modelContext.Miners, buildingDictionary);
        LinkMinersAndResources(modelContext.Miners.LoadAll(), modelContext.Resources.LoadAll(), itemDictionary, buildingDictionary);

        progress?.ReportOrThrow("Map Fuels", token);
        var fuels = MapToFuelModels(modelContext.Generators.LoadAll(), generators, itemDictionary);
        
        progress?.ReportOrThrow("Map Creatures", token);
        var creatures = MapToCreatures(modelContext.Creatures); 
        var creatureDictionary = MapToCreatureDictionary(creatures);
        LinkCreatureVariants(creatures, modelContext.Creatures, itemDictionary, creatureDictionary);
        var creatureLoots = MapToCreatureLoots(modelContext.Creatures, itemDictionary, creatureDictionary);

        progress?.ReportOrThrow("Map Recipes", token);
        var recipes = MapToRecipeModels(modelContext.Recipes.LoadAll(), itemDictionary, buildingDictionary);
        
        progress?.ReportOrThrow("Map References", token);
        var referenceDictionary = MapToEntityReferenceDictionary(itemDictionary, buildingDictionary, recipes, fuels, creatureLoots);

        var lastSyncDate = GetLastSyncDate();
        
        return new(
            itemDictionary.Values.ToArray(), 
            equipments, 
            consumables,
            weapons,
            ammunitions,
            resources,
            buildingDictionary.Values.ToArray(), 
            generators,
            miners,
            recipes,
            creatureDictionary.Values.ToArray(),
            referenceDictionary, 
            lastSyncDate);
    }

    #region Model Methods

    private IEnumerable<IItem> MapToItemModels(IEnumerable<Item> items) => items.Select(p => new Models.Item
    { 
        ClassName = p.ClassName,
        Name = p.Name,
        Description = p.Description,
        Form = p.Form,
        EnergyValue = p.EnergyValue,
        Image = BitmapImageCache.Fetch(SelectImagePath(p.SmallImagePath, p.BigImagePath))
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
    private IResource[] MapToResources(IEnumerable<Resource> resources, IDictionary<string, IItem> itemDictionary) => resources
        .Select(p => MapToResource(p, itemDictionary))
        .OrderBy(p => p.Name)
        .ToArray();

    private IResource MapToResource(Resource resource, IDictionary<string, IItem> itemDictionary)
    {
        var item = itemDictionary[resource.ClassName];
        
        var mappedResource = new Models.Resource
        {
            ClassName = item.ClassName, 
            Name = item.Name,
            Description = item.Description, 
            Form = item.Form, 
            EnergyValue = item.EnergyValue, 
            Image = item.Image
        };
        
        itemDictionary[resource.ClassName] = mappedResource;
        return mappedResource;
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
            mappedMunition.UsedInWeapon = (IWeapon)itemDictionary[ammunition.WeaponClassName];
        }
        
        foreach (var weapon in weapons)
        {
            var mappedWeapon = (IWeapon)itemDictionary[weapon.ClassName];
            mappedWeapon.Ammunitions = weapon.Ammunitions.Select(p => (IAmmunition)itemDictionary[p.ClassName]).ToArray();
        }
    }

    private IEnumerable<IBuilding> MapToBuildingModels(IEnumerable<Building> buildings)
    {
        return buildings.Select(p => 
            new Models.Building
            {
                ClassName = p.ClassName, 
                Name = p.Name,
                Description = p.Description, 
                Image = BitmapImageCache.Fetch(SelectImagePath(p.SmallImagePath, p.BigImagePath)),
                ManufactoringSpeed = p.ManufactoringSpeed, 
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
    
    private IMiner[] MapToMinerModels(IEnumerable<Miner> miner, IDictionary<string, IBuilding> buildingDictionary) => miner
        .Select(p => MapToMinerModel(p, buildingDictionary))
        .OrderBy(p => p.Name)
        .ToArray();

    private IMiner MapToMinerModel(Miner miner, IDictionary<string, IBuilding> buildingDictionary)
    {
        var buildingReference = buildingDictionary[miner.ClassName];
        
        var mappedMiner = new Models.Miner()
        {
            ClassName = buildingReference.ClassName, 
            Name = buildingReference.Name,
            Description = buildingReference.Description, 
            Image = buildingReference.Image,
            ManufactoringSpeed = buildingReference.ManufactoringSpeed, 
            PowerConsumption = buildingReference.PowerConsumption,
            PowerConsumptionExponent = buildingReference.PowerConsumptionExponent,
            PowerConsumptionRange = buildingReference.PowerConsumptionRange
        };
        
        buildingDictionary[miner.ClassName] = mappedMiner;
        return mappedMiner;
    }
    
    private void LinkMinersAndResources(IEnumerable<Miner> miners, IEnumerable<Resource> resources, IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary)
    {
        foreach (var resource in resources)
        {
            var mappedResource = (IResource)itemDictionary[resource.ClassName];
            mappedResource.Miners = resource.Miners.Select(p => (IMiner)buildingDictionary[p.ClassName]).ToArray();
        }
        
        foreach (var miner in miners)
        {
            var mappedMiner = (IMiner)buildingDictionary[miner.ClassName];
            mappedMiner.Resources = miner.ExtractableResources.Select(p => (IResource)itemDictionary[p.ClassName]).ToArray();
        }
    }

    private GeneratorFuel[] MapToFuelModels(IQueryable<Generator> generators, IGenerator[] generatorModels, IDictionary<string, IItem> itemDictionary)
    {
        List<GeneratorFuel> fuelModels = new();
        foreach (var generator in generators)
        {
            var generatorModel = generatorModels.First(p => p.ClassName == generator.ClassName);
            fuelModels.AddRange(generator.Fuels.Select(fuel => MapToFuelModel(fuel, generatorModel, itemDictionary)));
        }
        return fuelModels.ToArray();
    }
    
    private GeneratorFuel MapToFuelModel(FuelItem fuel, IGenerator generator, IDictionary<string, IItem> itemDictionary)
    {
        var ingredient = MapToFuelContentModel(itemDictionary[fuel.Fuel.ClassName], 0);

        var supplementalIngredient = !string.IsNullOrEmpty(fuel.Supplement?.ClassName)
            ? MapToFuelContentModel(itemDictionary[fuel.Supplement!.ClassName], 0)
            : null;

        SatisfactoryCalculator.Source.Models.FuelItem? byProductItem = null;
        decimal? byProductAmount = null;

        if (!string.IsNullOrEmpty(fuel.ByProduct?.ClassName))
        {
            byProductItem = MapToFuelContentModel(itemDictionary[fuel.ByProduct.ClassName], 0);
            byProductAmount = fuel.ByProductAmount;
        }

        var generatorFuel = new GeneratorFuel()
        {
            Ingredient = ingredient, 
            SupplementalIngredient = supplementalIngredient, 
            ByProduct = byProductItem,
            ByProductAmount = byProductAmount, 
            Generator = generator
        };

        _modelCalculationService.CalculateAndApplyRoundedFuelConsumption(100, generatorFuel);
        
        return generatorFuel;
    }

    private SatisfactoryCalculator.Source.Models.FuelItem MapToFuelContentModel(IItem item, decimal amount) => new(item, amount);
    
    // ReSharper disable once HeapView.ClosureAllocation
    private ICreature[] MapToCreatures(IEnumerable<Creature> creatues) => creatues
        .Select(MapToCreature)
        .OrderBy(p => p.Name)
        .ToArray();

    private ICreature MapToCreature(Creature creatue) =>
        new Models.Creature
        {
            ClassName = creatue.ClassName, 
            Name = creatue.Name,
            Description = creatue.Description,
            Image = BitmapImageCache.Fetch(SelectImagePath(creatue.SmallImagePath, creatue.BigImagePath)),
            HitPoints = creatue.HitPoints,
            Damage = creatue.Damage.ToArray(),
            Behaviour = creatue.Behaviour
        };

    private void LinkCreatureVariants(ICreature[] creatureModels, IEnumerable<Creature> creatures, IDictionary<string, IItem> itemDictionary, IDictionary<string, ICreature> creatureDictionary)
    {
        foreach (var creature in creatures)
        {
            var creatureModel = creatureDictionary[creature.ClassName];
            creatureModel.Variants = MapToVariants(creatures, creatureModel.ClassName, creatureDictionary);
        }
    }
    
    private ICreature[]? MapToVariants(IEnumerable<Creature> creatures, string creatureClassName, IDictionary<string, ICreature> creatureDictionary)
    {
        return
            creatures
                .Where(p => p.ClassName == creatureClassName)
                .SelectMany(p => p.Variants)
                .Select(p => creatureDictionary[p.ClassName])
                .OrderBy(p => p.ClassName)
                .ToArray();
    }

    private Models.CreatureLoot[] MapToCreatureLoots(IEnumerable<Creature> creatures, IDictionary<string, IItem> itemDictionary, IDictionary<string, ICreature> creatureDictionary) =>
        creatures
            .Where(p => p.Loot is not null)
            .Select(p =>
                MapToLoot(p.Loot!.Amount, creatureDictionary[p.ClassName], itemDictionary[p.Loot.ItemClassName]))
            .ToArray();

    private Models.CreatureLoot MapToLoot(int amount, ICreature mappedCreature, IItem mappedItem) =>
        new()
        {
            Item = mappedItem,
            Amount = amount,
            Creature = mappedCreature
        };

    private IRecipe[] MapToRecipeModels(IEnumerable<Recipe> recipes, IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary) =>
        recipes
            .Select(p => MapToRecipeModel(p, itemDictionary, buildingDictionary))
            .OrderRecipeModel();

    private IRecipe MapToRecipeModel(Recipe recipe, IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary)
    {
        var buildings = recipe.Buildings
            .Select(building => MapToRecipeBuildingModel(buildingDictionary[building.ClassName], recipe.VariablePowerConsumptionRange))
            .ToList();
        
        if (recipe.ConstructedInWorkbench)
            buildings.Add(MapToRecipeBuildingModel(buildingDictionary["WorkBench"], recipe.VariablePowerConsumptionRange));
        
        if (recipe.ConstructedInWorkshop)
            buildings.Add(MapToRecipeBuildingModel(buildingDictionary["Workshop"], recipe.VariablePowerConsumptionRange));
    
        var productIsBuilding = recipe.Products
            .Any(p => p.BuildingClassName is not null && buildingDictionary.ContainsKey(p.BuildingClassName));
    
        var ingredients = recipe.Ingredients
            .Select(ingredient => MapToRecipeContentModel(ingredient, recipe.ManufactoringDuration, itemDictionary, productIsBuilding))
            .ToArray();
        
        var products = recipe.Products
            .Select(product => MapToRecipeContentModel(product, recipe.ManufactoringDuration, itemDictionary, buildingDictionary, productIsBuilding))
            .ToArray();
        
        return new Models.Recipe
        {
            ClassName = recipe.ClassName, 
            Name = recipe.Name,
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

    private RecipePart MapToRecipeContentModel(RecipeIngredient recipeIngredient, decimal manufactoringDuration, IDictionary<string, IItem> itemDictionary, bool setOnlyAmount)
    {
        IEntity? recipePart;
        Form? form = null;

        if (itemDictionary.TryGetValue(recipeIngredient.ItemClassName, out var item))
        {
            recipePart = item;
            form = item.Form;
        }
        else
        {
            throw new("Recipe Ingredient not found in items");
        }

        return new(recipePart,
            form.NormalizeAmount(recipeIngredient.Amount), recipeIngredient.Amount,
            setOnlyAmount
                ? null
                : _modelCalculationService.CalculateAmountPerMinte(form, recipeIngredient.Amount, manufactoringDuration));
    }
    
    private RecipePart MapToRecipeContentModel(RecipeProduct recipeProduct, decimal manufactoringDuration, IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary, bool setOnlyAmount)
    {
        IEntity? recipePart;
        Form? form = null;
        
        if(recipeProduct.ItemClassName is not null && itemDictionary.TryGetValue(recipeProduct.ItemClassName, out var item))
        {
            recipePart = item;
            form = item.Form;
            if (item.ClassName == "OreBauxite")
                _ = "";
        }
        else if (recipeProduct.BuildingClassName is not null && buildingDictionary.TryGetValue(recipeProduct.BuildingClassName, out var building))
            recipePart = building;
        else
            throw new("Recipe Product not found in items or buildings");
        
        return new(recipePart,
            form.NormalizeAmount(recipeProduct.Amount), recipeProduct.Amount,
            setOnlyAmount
                ? null
                : _modelCalculationService.CalculateAmountPerMinte(form, recipeProduct.Amount, manufactoringDuration));
    }

    private RecipeBuilding MapToRecipeBuildingModel(IBuilding building, PowerConsumptionRange? powerConsumptionRange) =>
        new(building, powerConsumptionRange);

    #endregion

    #region Dictionary Methods

    private IDictionary<string, IItem> MapToItemDictionary(IEnumerable<IItem> items) =>
        items.ToDictionary(p => p.ClassName, p => p);

    private IDictionary<string, IBuilding> MapToBuildingDictionary(IEnumerable<IBuilding> buildings) =>
        buildings.ToDictionary(p => p.ClassName, p => p);
    
    private IDictionary<string, ICreature> MapToCreatureDictionary(IEnumerable<ICreature> creatures) =>
        creatures.ToDictionary(p => p.ClassName, p => p);

    // ReSharper disable once HeapView.ClosureAllocation
    private IDictionary<string, EntityReference> MapToEntityReferenceDictionary(IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary, IRecipe[] recipes, GeneratorFuel[] fuels, Models.CreatureLoot[] creatureLoots)
    {
        var entityReferences = itemDictionary.Values
            .ToDictionary(item => item.ClassName, item => CreateEntityReference(item.ClassName, buildingDictionary, fuels, recipes));

        foreach (var building in buildingDictionary.Values)
            entityReferences.Add(building.ClassName, CreateEntityReference(building.ClassName, buildingDictionary, fuels, recipes));

        return entityReferences;
    }

    private EntityReference CreateEntityReference(string entityClassName, IDictionary<string, IBuilding> buildingDictionary, GeneratorFuel[] fuels, IRecipe[] recipes) =>
        new()
        {
            RecipeIngredient = GetRecipeIngredientReferences(entityClassName, recipes, buildingDictionary),
            RecipeBuildingIngredient = GetRecipeBuildingIngredientReferences(entityClassName, recipes, buildingDictionary),
            RecipeProduct = GetRecipeProductReferences(entityClassName, recipes),
            RecipeBuilding = GetRecipeBuildingReferences(entityClassName, recipes),
            FuelIngredient = GetFuelIngredientReferences(entityClassName, fuels),
            FuelByProduct = GetFuelByProductReferences(entityClassName, fuels),
            FuelGenerator = GetFuelGeneratorReferences(entityClassName, fuels),
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

    private GeneratorFuel[] GetFuelIngredientReferences(string entityClassName, IEnumerable<GeneratorFuel> fuels) => fuels
        .Where(fuel => fuel.Ingredient.Item.ClassName == entityClassName || fuel.SupplementalIngredient?.Item.ClassName == entityClassName)
        .ToArray();

    private GeneratorFuel[] GetFuelByProductReferences(string entityClassName, IEnumerable<GeneratorFuel> fuels) => fuels
       .Where(fuel => fuel.ByProduct is not null && fuel.ByProduct.Item.ClassName == entityClassName)
       .ToArray();

    private GeneratorFuel[] GetFuelGeneratorReferences(string entityClassName, IEnumerable<GeneratorFuel> fuels) => fuels
       .Where(fuel => fuel.Generator.ClassName == entityClassName)
       .ToArray();
    
    private Models.CreatureLoot[] GetCreatureDropsLootReferences(string entityClassName, IEnumerable<Models.CreatureLoot> creatureLoots) => creatureLoots
        .Where(creatureLoot => creatureLoot.Creature.ClassName == entityClassName)
        .ToArray();
    
    private Models.CreatureLoot[] GetItemIsDroppedFromCreatureReferences(string entityClassName, IEnumerable<Models.CreatureLoot> creatureLoots) => creatureLoots
        .Where(creatureLoot => creatureLoot.Item.ClassName == entityClassName)
        .ToArray();

    #endregion

    #region Utility Methods

    private string SelectImagePath(string smallIconPath, string bigIconPath) => string.IsNullOrEmpty(smallIconPath)
        ? bigIconPath
        : smallIconPath;

    // private DateTime? GetLastSyncDate() => File.Exists(Constants.InformationFileName)
    //     ? new DateTime?(File.GetLastWriteTime(Constants.InformationFileName))
    //     : null;
    //
    private DateTime? GetLastSyncDate() => DateTime.Now;

    #endregion

    private readonly ModelCalculationService _modelCalculationService;
    private readonly IDbContextFactory<ModelContext> _modelContextFactory;
}