using Ammunition = Data.Models.Implementation.Ammunition;
using Building = Data.Models.Implementation.Building;
using Consumable = Data.Models.Implementation.Consumable;
using Creature = Data.Models.Implementation.Creature;
using CreatureLoot = Data.Models.Implementation.CreatureLoot;
using Equipment = Data.Models.Implementation.Equipment;
using FuelItem = Data.Models.Implementation.FuelItem;
using Generator = Data.Models.Implementation.Generator;
using Item = Data.Models.Implementation.Item;
using Recipe = Data.Models.Implementation.Recipe;
using Weapon = Data.Models.Implementation.Weapon;

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

    public DataModelMappingResult? MapToConfigurationModel(DataContainer? data, IExtendedProgress<string>? progress = null, CancellationToken? token = null)
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
        
        progress?.ReportOrThrow("Map Creatures", token);
        var creatures = MapToCreatures(data.Creatures); 
        var creatureDictionary = MapToCreatureDictionary(creatures);
        LinkCreatureVariants(creatures, data.Creatures, itemDictionary, creatureDictionary);
        //var creatureLoots = MapToCreatureLoots(data.Creatures, itemDictionary, creatureDictionary);
        

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
            //mappedMunition.UsedInWeapon = (IWeapon)itemDictionary[ammunition.UsedInWeapon];
        }
        
        foreach (var weapon in weapons)
        {
            var mappedWeapon = (IWeapon)itemDictionary[weapon.ClassName];
            //mappedWeapon.Ammunitions = weapon.UsesAmmunition.Select(p => (IAmmunition)itemDictionary[p]).ToArray();
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

    private GeneratorFuel[] MapToFuelModels(IEnumerable<Generator> generators, IGenerator[] generatorModels, IDictionary<string, IItem> itemDictionary)
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
        
        if (string.IsNullOrEmpty(fuel.ByProduct?.ClassName))
            return new()
            {
                Ingredient = ingredient, 
                SupplementalIngredient = supplementalIngredient, 
                ByProduct = byProductItem,
                ByProductAmount = byProductAmount, 
                Generator = generator
            };
        
        byProductItem = MapToFuelContentModel(itemDictionary[fuel.ByProduct.ClassName], 0);
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
            //creatureModel.Variants = MapToVariants(creatureModels, creatures, creatureModel.ClassName, creature.VariantGroup, creatureDictionary);
        }
    }
    
    // private ICreature[]? MapToVariants(IEnumerable<ICreature> creatureModels, IEnumerable<Creature> creatures, string creatureClassName, string? variantGroup, IDictionary<string, ICreature> creatureDictionary)
    // {
    //     if (variantGroup is null)
    //         return null;
    //
    //     var foundCreatureClassNames =
    //         creatures
    //             .Where(p => p.VariantGroup == variantGroup && p.ClassName != creatureClassName)
    //             .Select(p => p.ClassName);
    //
    //     return foundCreatureClassNames
    //         .Select(p => creatureDictionary[p])
    //         .OrderBy(p => p.ClassName)
    //         .ToArray();
    // }

    // private Models.CreatureLoot[] MapToCreatureLoots(IEnumerable<Creature> creatures, IDictionary<string, IItem> itemDictionary, IDictionary<string, ICreature> creatureDictionary)
    // {
    //     List<Models.CreatureLoot> creatureLoots = new();
    //     foreach (var creature in creatures)
    //     {
    //         var creatureModel = creatureDictionary[creature.ClassName];
    //         creatureLoots.AddRange(creature.Loot.Select(loot => MapToLoot(loot.Amount, creatureModel, itemDictionary[loot.ClassName])));
    //     }
    //     return creatureLoots.ToArray();
    // }
    
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
        // var buildings = recipe.Buildings
        //     .Select(building => MapToRecipeBuildingModel(buildingDictionary[building], recipe.VariablePowerConsumptionRange))
        //     .ToList();
        //
        // if (recipe.ConstructedInWorkbench)
        //     buildings.Add(MapToRecipeBuildingModel(buildingDictionary["WorkBench_C"], recipe.VariablePowerConsumptionRange));
        //
        // if (recipe.ConstructedInWorkshop)
        //     buildings.Add(MapToRecipeBuildingModel(buildingDictionary["Workshop_C"], recipe.VariablePowerConsumptionRange));

        // var productIsBuilding = recipe.Products
        //     .Any(p => buildingDictionary.ContainsKey(p.Item.ClassName));

        // var ingredients = recipe.Ingredients
        //     .Select(ingredient => MapToRecipeContentModel(ingredient, recipe.ManufactoringDuration, itemDictionary, buildingDictionary, productIsBuilding))
        //     .ToArray();
        //
        // var products = recipe.Products
        //     .Select(product => MapToRecipeContentModel(product, recipe.ManufactoringDuration, itemDictionary, buildingDictionary, productIsBuilding))
        //     .ToArray();
        
        return new Models.Recipe
        {
            ClassName = recipe.ClassName, 
            Name = recipe.Name,
            IsAlternate = recipe.IsAlternate, 
            ConstructedByBuildGun = recipe.ConstructedByBuildGun,
            ConstructedInWorkbench = recipe.ConstructedInWorkbench, 
            ConstructedInWorkshop = recipe.ConstructedInWorkshop,
            ManufactoringDuration = recipe.ManufactoringDuration, 
            // Ingredients = ingredients,
            // Products = products,
            // Buildings = buildings.ToArray()
        };
    }

    // private RecipePart MapToRecipeContentModel(Reference recipeItem, double manufactoringDuration, IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary, bool setOnlyAmount)
    // {
    //     IEntity? recipePart;
    //     Form? form = null;
    //     if(itemDictionary.TryGetValue(recipeItem.ClassName, out var item))
    //     {
    //         recipePart = item;
    //         form = item.Form;
    //     }
    //     else
    //     {
    //         recipePart = buildingDictionary[recipeItem.ClassName];
    //     }
    //
    //     return new(recipePart,
    //         _calculationService.NormalizeAmount(form, recipeItem.Amount), recipeItem.Amount,
    //         setOnlyAmount
    //             ? null
    //             : _calculationService.CalculateAmountPerMinte(form, recipeItem.Amount, manufactoringDuration));
    // }

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
    private IDictionary<string, EntityReference> MapToEntityReferenceDictionary(IDictionary<string, IItem> itemDictionary, IDictionary<string, IBuilding> buildingDictionary, GeneratorFuel[] fuels, IRecipe[] recipes)
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

    private readonly CalculationService _calculationService;
}