using Data.Context;
using Data.Models.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SatisfactoryCalculator.Shared.Models;
using SatisfactoryCalculator.Shared.Services;

namespace SatisfactoryCalculator.Tests;

public class FactoryConfigTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly ModelContext _context;
    
    public FactoryConfigTests()
    {
        var pathOptions = CreatePathOptions();
        
        var services = new ServiceCollection();
        services.Configure<PathOptions>(options =>
        {
            options.DataFolder = pathOptions.DataFolder;
            options.ImageFolder = pathOptions.ImageFolder;
        });
        
        services.AddDbContextFactory<ModelContext>(options =>
            options
                .UseSqlite($@"Data Source={pathOptions.DataFile};Pooling=false")
        );
        
        services
            .AddTransient<CalculationService>();
        
        _serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true,
            ValidateScopes = false
        });
        
        _context = _serviceProvider.GetRequiredService<ModelContext>();
    }

    private PathOptions CreatePathOptions()
    {
        var dataFolder = Environment.CurrentDirectory + "\\Data";
        var imageFolder = dataFolder + "\\Images";

        return new PathOptions
        {
            DataFolder = dataFolder,
            ImageFolder = imageFolder
        };
    }
    
    [Theory(DisplayName = "Assert Iron Plate recipe list returns correctly")]
    [InlineData("IronPlate", "IronPlate")]
    [InlineData("Cable", "Cable")]
    [InlineData("NuclearFuelRod", "NuclearFuelRod")]
    public async Task AssertIronPlateRecipeListReturnsCorrectAmount(string itemClassName, string expectedNonAlternateRecipe)
    {
        var recipeList = new List<Recipe>();
        await CreateAllPossibleRecipesList(itemClassName, recipeList);
        Assert.True(recipeList.Count > 100); //a lot of recipes should be expected due to alternative recipes creating a very convoluted dependency tree
        Assert.Contains(recipeList, recipe => !recipe.IsAlternate && recipe.ClassName == expectedNonAlternateRecipe);
    }
    
    /// <param name="itemClassName"></param>
    /// <param name="allowAltRecipes"></param>
    /// <param name="expectedRecipes">Format: put recipes in order of recipes to be found. if found recipe dependency has one recipe on the 1st level and 2 on the 2nd level put it in this order 1 (1st) 2(2nd) 3(2nd)</param>
    [Theory(DisplayName = "Assert Dependency Tree is correct")]
    [InlineData("IronPlate", false, "IronPlate", "IngotIron")]
    ////
    public async Task AssertDependencyTreeCorrect(string itemClassName, bool allowAltRecipes, params string[] expectedRecipes)
    {
        var recipeList = new List<Recipe>();
        await CreateAllPossibleRecipesList(itemClassName, recipeList);
        
        if (!allowAltRecipes)
            recipeList = recipeList.Where(p => !p.IsAlternate).ToList();

        var dependencyTrees = CreateDependencyTree(itemClassName, recipeList)!;
        
        foreach (var dependencyTree in dependencyTrees)
        {
            var startIndex = 0;
            RecursivelyAssertDependencyTree(dependencyTree, expectedRecipes, ref startIndex);
        }
    }

    private void RecursivelyAssertDependencyTree(RecipeDependency recipeDependency, string[] expectedOutput, ref int currentExpectedOutputIndex)
    {
        Assert.Equal(expectedOutput[currentExpectedOutputIndex], recipeDependency.ClassName);
        foreach (var dependency in recipeDependency.Dependencies)
        {
            currentExpectedOutputIndex++;
            RecursivelyAssertDependencyTree(dependency, expectedOutput, ref currentExpectedOutputIndex);
        }
    }

    public List<RecipeDependency> CreateDependencyTree(string itemClassName, List<Recipe> recipeList)
    {
        var recipes = recipeList.Where(recipe => recipe.Products.Any(recipeProduct => recipeProduct.ItemClassName == itemClassName)).ToList();

        if (recipes.Count == 0)
            return [];

        var recipeDependencies = new List<RecipeDependency>(recipes.Select(recipe => new RecipeDependency(recipe)));

        foreach (var recipeDependency in recipeDependencies)
            foreach (var ingredient in recipeDependency.Ingredients)
                recipeDependency.Dependencies.AddRange(CreateDependencyTree(ingredient.ItemClassName, recipeList));
        
        return recipeDependencies;
    }

    public class RecipeDependency : Recipe
    {
        public List<RecipeDependency> Dependencies { get; set; } = [];
        public RecipeDependency(Recipe source)
        {
            ClassName = source.ClassName;
            Buildings = source.Buildings;
            Ingredients = source.Ingredients;
            IsAlternate = source.IsAlternate;
            Products = source.Products;
            Name = source.Name;
            ManufactoringDuration = source.ManufactoringDuration;
            ConstructedInWorkbench = source.ConstructedInWorkbench;
            ConstructedInWorkshop = source.ConstructedInWorkshop;
            ManualManufacturingMultiplier = source.ManualManufacturingMultiplier;
            ManufacturingMenuPriority = source.ManufacturingMenuPriority;
            ConstructedByBuildGun = source.ConstructedByBuildGun;
            VariablePowerConsumptionRange = source.VariablePowerConsumptionRange;
        }
    }
    
    private async Task CreateAllPossibleRecipesList(string itemClassName, ICollection<Recipe> recipeList)
    {
        var recipes = await GetRecipes(itemClassName);
        foreach (var recipe in recipes)
        {
            if (recipeList.Any(p => p.ClassName == recipe.ClassName)) 
                continue;
            
            recipeList.Add(recipe);
            foreach (var ingredient in recipe.Ingredients)
                await CreateAllPossibleRecipesList(ingredient.ItemClassName, recipeList);
        }
    }
    
    private async Task<Recipe[]> GetRecipes(string itemClassName, bool includeAlternates = true) =>
        await _context.Recipes
            .Where(recipe => recipe.Products.Any(recipeProduct => recipeProduct.ItemClassName == itemClassName) && (includeAlternates || !recipe.IsAlternate))
            .Include(p => p.Buildings)
            .Include(p => p.Ingredients)
            .Include(p => p.Products)
            .ToArrayAsync();
}