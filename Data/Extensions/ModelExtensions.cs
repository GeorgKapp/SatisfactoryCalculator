namespace Data.Extensions.Model;

public static class ModelExtensions
{
    public static IQueryable<Ammunition> LoadAll(this IQueryable<Ammunition> ammunitions) =>
        ammunitions
            .Include(p => p.Item)
            .Include(p => p.Weapon);

    public static IQueryable<Building> LoadAll(this IQueryable<Building> buildings) =>
        buildings
            .Include(p => p.Generator)
            .Include(p => p.Miner);

    public static IQueryable<Consumable> LoadAll(this IQueryable<Consumable> consumables) =>
        consumables
            .Include(p => p.Item);

    public static IQueryable<Creature> LoadAll(this IQueryable<Creature> creatures) =>
        creatures
            .Include(p => p.Variants)
            .Include(p => p.Loot);

    public static IQueryable<CreatureLoot> LoadAll(this IQueryable<CreatureLoot> creatureLoots) =>
        creatureLoots
            .Include(p => p.Item);
    
    public static IQueryable<CustomizationRecipe> LoadAll(this IQueryable<CustomizationRecipe> customizationRecipes) =>
        customizationRecipes
            .Include(p => p.Ingredients);
    
    public static IQueryable<Equipment> LoadAll(this IQueryable<Equipment> equipments) =>
        equipments
            .Include(p => p.Item);
    
    public static IQueryable<FuelItem> LoadAll(this IQueryable<FuelItem> fuelItems) =>
        fuelItems
            .Include(p => p.Fuel)
            .Include(p => p.Supplement)
            .Include(p => p.ByProduct);

    public static IQueryable<Generator> LoadAll(this IQueryable<Generator> generators) =>
        generators
            .Include(p => p.Building)
            .Include(p => p.Fuels);
    
    public static IQueryable<Item> LoadAll(this IQueryable<Item> items) =>
        items
            .Include(p => p.Ammunition)
            .Include(p => p.Consumable)
            .Include(p => p.Equipment)
            .Include(p => p.Resource)
            .Include(p => p.Vehicle)
            .Include(p => p.Weapon);
    
    public static IQueryable<Miner> LoadAll(this IQueryable<Miner> miners) =>
        miners
            .Include(p => p.Building)
            .Include(p => p.ExtractableResources);
    
    public static IQueryable<Recipe> LoadAll(this IQueryable<Recipe> recipes) =>
        recipes
            .Include(p => p.Buildings)
            .Include(p => p.Ingredients)
                .ThenInclude(p => p.Item)
            .Include(p => p.Products)
                .ThenInclude(p => p.Item)
            .Include(p => p.Products)
                .ThenInclude(p => p.Building);
    
    public static IQueryable<RecipeIngredient> LoadAll(this IQueryable<RecipeIngredient> recipeIngredients) =>
        recipeIngredients
            .Include(p => p.Item);
    
    public static IQueryable<RecipeProduct> LoadAll(this IQueryable<RecipeProduct> recipeProducts) =>
        recipeProducts
            .Include(p => p.Item)
            .Include(p => p.Building);
    
    public static IQueryable<Resource> LoadAll(this IQueryable<Resource> resources) =>
        resources
            .Include(p => p.Item)
            .Include(p => p.Miners);
    
    public static IQueryable<ScannableObject> LoadAll(this IQueryable<ScannableObject> scannableObjects) =>
        scannableObjects
            .Include(p => p.Item)
            .Include(p => p.Creature)
            .Include(p => p.Plant)
            .Include(p => p.ScanningActors)
                .ThenInclude(p => p.Item)
            .Include(p => p.ScanningActors)
                .ThenInclude(p => p.Building);
    
    public static IQueryable<ScanningActor> LoadAll(this IQueryable<ScanningActor> scanningActors) =>
        scanningActors
            .Include(p => p.Item)
            .Include(p => p.Building);
    
    public static IQueryable<Schematic> LoadAll(this IQueryable<Schematic> schematics) =>
        schematics
            .Include(p => p.Costs)
            .Include(p => p.Dependencies)
            .Include(p => p.UnlocksScannableObjects)
            .Include(p => p.UnlocksRecipes)
            .Include(p => p.UnlocksScannerResources)
            .Include(p => p.UnlocksScannerResourcePairs)
            .Include(p => p.UnlocksEmotes)
            .Include(p => p.GivesItems);
    
    public static IQueryable<SchematicCost> LoadAll(this IQueryable<SchematicCost> schematicCosts) =>
        schematicCosts
            .Include(p => p.Item);
    
    public static IQueryable<SchematicDependency> LoadAll(this IQueryable<SchematicDependency> schematicDependencies) =>
        schematicDependencies
            .Include(p => p.Schematics);
    
    public static IQueryable<Vehicle> LoadAll(this IQueryable<Vehicle> vehicles) =>
        vehicles
            .Include(p => p.Item);
    
    public static IQueryable<Weapon> LoadAll(this IQueryable<Weapon> weapons) =>
        weapons
            .Include(p => p.Item)
            .Include(p => p.Ammunitions);
    
    public static IQueryable<FactoryConfiguration> LoadAll(this IQueryable<FactoryConfiguration> factoryConfigurations) =>
        factoryConfigurations
            .Include(p => p.FactoryBuildingConfigurations)
                .ThenInclude(p => p.Building)
            .Include(p => p.FactoryBuildingConfigurations)
                .ThenInclude(p => p.ProducedBuilding)
            .Include(p => p.FactoryBuildingConfigurations)
                .ThenInclude(p => p.ProducedItem)
            .Include(p => p.DesiredOutputs)
                .ThenInclude(p => p.Building)
            .Include(p => p.DesiredOutputs)
                .ThenInclude(p => p.Item)
            .Include(p => p.DesiredOutputs)
                .ThenInclude(p => p.AlternateRecipe);
}
