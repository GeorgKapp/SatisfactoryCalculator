using System.Diagnostics;
using System.Windows.Documents;
using Data.Context;
using Data.Models.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

    [Fact(DisplayName = "Calculate Factory Config Output for 100 Iron Plates")]
    public async Task CalculateFactoryConfigOutputFor100IronPlates()
    {
        var itemClassName = "IronPlate";
        var amountPerMinute = 100;

        // var usedRecipes = new Dictionary<string, Recipe>();
        // var recipeTree = await CalculateRecipeTree(itemClassName, usedRecipes);

        var ingredientDic = new Dictionary<string, Recipe[]>();
        await GetAllPotentialRecipes(itemClassName, ingredientDic);
        
        //put recipes into a dependency tree
        //first initialize all recipes into a dependency list
        //second link the dependency list into each other
        
        var recipeKeys = ingredientDic.Values.SelectMany(p => p).Select(p => p.ClassName).Distinct().ToArray();
        var recipesFetched = _context.Recipes.Where(p => recipeKeys.Contains(p.ClassName)).ToArray();
        
        var recipeList = new List<RecipeTree>();
        foreach (var recipe in recipesFetched)
        {
            recipeList.Add(new RecipeTree
            {
               Recipe =recipe,
               DependendOn = new List<RecipeTree>()
            });
        }

        // foreach (var recipeListEntry in recipeList)
        // {
        //     foreach (var ingredient in recipeListEntry.Item1.Ingredients)
        //     {
        //         foreach (var recipeListPotentialRelatedEntry in recipeList)
        //         {
        //             if (recipeListPotentialRelatedEntry.Item1.Products.Any(p => p.ItemClassName == ingredient.ItemClassName))
        //             {
        //                 recipeListEntry.Item2.Add(recipeListPotentialRelatedEntry);
        //             }
        //         }
        //     }
        // }
        //
        _ = "";
    }

    [Fact(DisplayName = "2. Calculate Factory Config Output for 100 Iron Plates")]
    public async Task CalculateFactoryConfigOutputFor100IronPlates2()
    {
        var itemClassName = "IronPlate";
        var recipeList = new List<RecipeWithDependency>();
        
        var result = await TestLinking(itemClassName, recipeList);
    }

    private async Task<List<RecipeWithDependency>> TestLinking(string itemClassName, List<RecipeWithDependency> recipeList)
    {
        var recipes = await GetRecipes(itemClassName);
        var dependencies = new List<RecipeWithDependency>();
        
        foreach (var recipe in recipes)
        {
            dependencies.Add(new RecipeWithDependency(recipe));
        }
        
        recipeList.AddRange(dependencies);

        return dependencies;
    }

    private void TryAddToRecipeList(List<Recipe> recipeList, Recipe recipe)
    {
        recipeList.Add(recipe);
        recipeList = recipeList.DistinctBy(p => p.ClassName).ToList();
    }

    public class DependencyTree
    {
        public Recipe Recipe { get; set; }
        public List<DependencyTree> Dependencies { get; set; } = new();
    }

    public class RecipeWithDependency : Recipe
    {
        //Dependencies are basically potential recipes related to ingredients
        public List<RecipeWithDependency> Dependencies { get; set; }

        public RecipeWithDependency(Recipe recipe)
        {
            this.ClassName = recipe.ClassName;
            this.Name = recipe.Name;
            this.ManualManufacturingMultiplier = recipe.ManualManufacturingMultiplier;
            this.ManufactoringDuration = recipe.ManufactoringDuration;
            this.ManufacturingMenuPriority = recipe.ManufacturingMenuPriority;
            this.ConstructedByBuildGun = recipe.ConstructedByBuildGun;
            this.ConstructedInWorkshop = recipe.ConstructedInWorkshop;
            this.ConstructedInWorkbench = recipe.ConstructedInWorkbench;
            this.IsAlternate = recipe.IsAlternate;
            this.VariablePowerConsumptionRange = recipe.VariablePowerConsumptionRange;
            this.Buildings = recipe.Buildings;
            this.Ingredients = recipe.Ingredients;
            this.Products = recipe.Products;

            Dependencies = new List<RecipeWithDependency>();
        }
    }

    public async Task GetAllPotentialRecipes(string itemClassName, Dictionary<string, Recipe[]> ingredientDic)
    {
        var recipes = await GetRecipes(itemClassName);

        ingredientDic.TryAdd(itemClassName, []);
        ingredientDic[itemClassName] = recipes;

        foreach (var recipe in recipes)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
               if(ingredientDic.ContainsKey(ingredient.ItemClassName))
                   continue;
               
               await GetAllPotentialRecipes(ingredient.ItemClassName, ingredientDic);
            }
        }
    }

    public async Task<List<RecipeTree>> CalculateRecipeTree(string itemClassName, Dictionary<string, Recipe> usedRecipes)
    {
        var recipes = await GetRecipes(itemClassName);
        
        var recipeTrees = new List<RecipeTree>();
        foreach (var recipe in recipes)
        {
            if (!usedRecipes.TryAdd(recipe.ClassName, recipe))
                continue;

            var recipeTreeDependencies = new List<RecipeTree>();
            foreach (var ingredient in recipe.Ingredients)
            {
                recipeTreeDependencies.AddRange(await CalculateRecipeTree(ingredient.ItemClassName, usedRecipes));
            }
            
            recipeTrees.Add(new RecipeTree
            {
                Recipe = recipe,
                DependendOn = recipeTreeDependencies
            });
        }

        return recipeTrees;
    }

    private async Task<Recipe[]> GetRecipes(string itemClassName) =>
        await _context.Recipes
            .Where(recipe => recipe.Products.Any(recipeProduct => recipeProduct.ItemClassName == itemClassName))
            .Include(p => p.Buildings)
            .Include(p => p.Ingredients)
            .Include(p => p.Products)
            .ToArrayAsync();

    public class RecipeTree
    {
        public Recipe Recipe { get; set; }
        public List<RecipeTree> DependendOn { get; set; }
    }
}