namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal class FactoryCalculationService
{
    public FactoryCalculationService(
        ApplicationState applicationState,
        ModelCalculationService modelCalculationService)
    {
        _applicationState = applicationState;
        _modelCalculationService = modelCalculationService;
    }

    public Result CalculateFactoryConfiguration(FactoryConfiguration factoryConfiguration)
    {
        var validationResult = ValidateFactoryConfiguration(factoryConfiguration);
        if (!validationResult.IsSuccess)
            return validationResult;
        
        foreach (var outputRequirement in factoryConfiguration.OutputRequirements)
        {
            var recipeDependency = BuildRecipeDependencyTree(outputRequirement);
            var calculateProductionOutput = CalculateProductionOutput(recipeDependency, outputRequirement.Entity!);
            _ = "";
        }

        factoryConfiguration.CalculatedPowerConsumption =
            factoryConfiguration.FactoryBuildingConfigurations.Sum(p => p.EnergyConsumption);
        
        factoryConfiguration.CalculatedInVersion = _satisfactoryVersion;
        
        return Result.Success();
    }

    private RecipeDependency BuildRecipeDependencyTree(OutputRequirements outputRequirement)
    {
        var recipeDependency = new RecipeDependency
        {
            Recipe = outputRequirement.ChosenRecipe
        };

        BuildRecipeDependencyTree(recipeDependency);
        return recipeDependency;
    }
    
    private void BuildRecipeDependencyTree(RecipeDependency recipeDependency)
    {
        foreach (var ingredient in recipeDependency.Recipe.Ingredients)
        {
            var ingredientRecipe = GetEntityDefaultRecipe(ingredient.Part);
            if (ingredientRecipe is null)
                continue;
            
            var childRecipeDependency = new RecipeDependency
            {
                Recipe = ingredientRecipe
            };
            
            BuildRecipeDependencyTree(childRecipeDependency);
            recipeDependency.Dependencies.Add(childRecipeDependency);
        }
    }
    
    private IRecipe? GetEntityDefaultRecipe(IEntity entity)
    {
        if (entity is IResource)
            return null;
        
        var recipeProducts = _applicationState.Configuration.ReferenceDictionary[entity.ClassName].RecipeProduct;

        return recipeProducts
            .FirstOrDefault(p => !p.IsAlternate);
    }

    private ProductionOutput CalculateProductionOutput(RecipeDependency recipeDependency, IEntity producedEntity)
    {
        var productionOutput = new ProductionOutput();
        
        var productionBuilding = recipeDependency.Recipe.Buildings.Single();
        
        
        
        foreach (var recipeProduct in recipeDependency.Recipe.Products)
        {
            
            
            
            if (recipeProduct.Part == producedEntity)
            {
                //Product
            }
            else
            {
                //By Product
                
            }
        }

        return productionOutput;
    }

    private decimal CalculateBuildingMultiplicator(IRecipe recipe, IEntity requiredEntity, decimal requiredAmountPerMinute)
    {
        var recipeBuilding = recipe.Buildings
            .First(p => p.Building.ClassName != "Workshop" && p.Building.ClassName != "WorkBench");

        var recipeItem = recipe.Products
            .First(p => p.Part == requiredEntity);
        
        
        
        return 0;
    }

    private Result ValidateFactoryConfiguration(FactoryConfiguration factoryConfiguration)
    {
        if(string.IsNullOrEmpty(factoryConfiguration.Name))
            return Result.Failure("Factory Configuration needs a name");
        
        if (factoryConfiguration.OutputRequirements.Count == 0)
            return Result.Failure("Configuration needs outputs to calculate");
        
        if(factoryConfiguration.OutputRequirements.Any(p => p.Entity is null))
            return Result.Failure("Every Output needs to have an item selected");
        
        if(factoryConfiguration.OutputRequirements.Any(p => p.Amount is null or 0M && p.BuildingAmount is null or 0M))
            return Result.Failure("Every Output needs at least a single amount filled");
        
        return Result.Success();
    }
    
    private const string _satisfactoryVersion = "0.7.1.1";
    
    private readonly ApplicationState _applicationState;
    private readonly ModelCalculationService _modelCalculationService;

    private class RecipeDependency
    {
        public IRecipe Recipe { get; set; }
        public List<RecipeDependency> Dependencies { get; set; } = new();
    }

    private class ProductionOutput
    {
        public IBuilding ProductionBuilding { get; set; }
        public IRecipe Recipe { get; set; }
        public decimal Overclock { get; set; }
        public decimal BuildingAmount { get; set; }

        public List<ItemSurplus> ItemSurplus { get; set; } = new();
        public List<ProductionOutput> ProductionOutputDependencies { get; set; } = new();
    }

    private class ItemSurplus
    {
        public IItem Item { get; set; }
        public decimal Amount { get; set; }
    }
}
