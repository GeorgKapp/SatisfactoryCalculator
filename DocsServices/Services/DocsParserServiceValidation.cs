// ReSharper disable HeapView.ObjectAllocation
namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private Result ValidateIfAllEntitiesWereTraversed(Dictionary<string, bool> rootObjectHandledDictionary)
    {
        var missingReferences = rootObjectHandledDictionary
            .Where(p => !p.Value)
            .Select(p => p.Key)
            .ToArray();

        return missingReferences.Length > 0 
            ? Result.Failure($"{string.Join(Environment.NewLine, missingReferences)} not parsed") 
            : Result.Success();
    }
    
    private static Result ValidateForDuplicates<T>(IEnumerable<T> source) where T : IClassNamePrimaryKey
    {
        var duplicates = source
            .GroupBy(p => p.ClassName)
            .Where(p => p.Count() > 1)
            .Select(p => p.Key)
            .ToArray();

        return duplicates.Length > 0
            ? Result.Failure($"Duplicate source Items in type: {typeof(T).Name} detected: {string.Join(Environment.NewLine, duplicates)}")
            : Result.Success();
    }

    private static Result ValidateReferencesByTarget(IEnumerable<string> source, IEnumerable<string> target)
    {
        var missingReferences = target.Except(source).ToArray();

        return missingReferences.Length > 0
            ? Result.Failure($"Reference Items missing: {string.Join(Environment.NewLine, missingReferences)}")
            : Result.Success();
    }

    private static Result SeperatelyValidateDataForDuplicates(TempModelContext data)
    {
        return Result.Combine(
            new[] 
            {
                ValidateForDuplicates(data.Items),
                ValidateForDuplicates(data.Recipes),
                ValidateForDuplicates(data.CustomizationRecipes),
                ValidateForDuplicates(data.Buildings),
                ValidateForDuplicates(data.Vehicles),
                ValidateForDuplicates(data.Miners),
                ValidateForDuplicates(data.Schematics),
                ValidateForDuplicates(data.Resources),
                ValidateForDuplicates(data.Consumables),
                ValidateForDuplicates(data.Ammunitions),
                ValidateForDuplicates(data.Weapons),
                ValidateForDuplicates(data.Equipments),
                ValidateForDuplicates(data.Generators),
                ValidateForDuplicates(data.Emotes),
                ValidateForDuplicates(data.Statues) }
            );
    }

    private static Result ValidateDataReferences(TempModelContext data)
    {
        var items = GetClassNames(data.Items);

        var itemReferences = GetClassNames(data.Consumables)
            .Concat(GetClassNames(data.Equipments))
            .Concat(GetClassNames(data.Weapons))
            .Concat(GetClassNames(data.Vehicles))
            .ToArray();

        var itemValidationResult = ValidateReferencesByTarget(items, itemReferences);
        if (!itemValidationResult.IsSuccess)
            return itemValidationResult;

        var buildings = GetClassNames(data.Buildings);

        var buildingReferences = GetClassNames(data.Generators)
            .Concat(GetClassNames(data.Miners))
            .ToArray();

        var buildingValidationResult = ValidateReferencesByTarget(buildings, buildingReferences);
        
        return !buildingValidationResult.IsSuccess 
            ? buildingValidationResult 
            : Result.Success();
    }

    private static Result ValidateItemExistanceInRecipes(TempModelContext data)
    {
        var sources = GetClassNames(data.Items)
            .Concat(GetClassNames(data.Vehicles))
            .Concat(GetClassNames(data.Consumables))
            .Concat(GetClassNames(data.Resources))
            .Concat(GetClassNames(data.Ammunitions))
            .Concat(GetClassNames(data.Equipments))
            .Concat(GetClassNames(data.Weapons))
            .Concat(GetClassNames(data.Buildings))
            .Concat(GetClassNames(data.Miners))
            .Concat(GetClassNames(data.Generators))
            .Concat(GetClassNames(data.Emotes))
            .Concat(GetClassNames(data.Statues))
            .ToArray();
        
        var recipeResults = new List<Result>();
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var recipe in data.Recipes)
        {
            var recipeContentNames = recipe.Ingredients.Select(p => p.ItemClassName)
                .Concat(recipe.Products.Select(p => string.IsNullOrEmpty(p.ItemClassName) ? p.BuildingClassName : p.ItemClassName))
                .Concat(recipe.Buildings.Select(p => p.ClassName))
                .ToArray();

            var differences = recipeContentNames.Except(sources).ToList();
            if (!differences.Any())
                continue;

            recipeResults.Add(Result.Failure(
                $"Recipe: {recipe.Name} could not find the following references: {string.Join(Environment.NewLine, differences)}"));
        }

        var customizationRecipeResults = new List<Result>();
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var customizationRecipe in data.CustomizationRecipes)
        {
            var recipeContentNames = customizationRecipe.Ingredients.Select(p => p.ItemClassName)
                .ToArray();

            var differences = recipeContentNames.Except(sources).ToList();
            if (!differences.Any())
                continue;

            customizationRecipeResults.Add(Result.Failure(
                $"Recipe: {customizationRecipe.Name} could not find the following references: {string.Join(Environment.NewLine, differences)}"));
        }
        
        return Result.Combine(recipeResults.Concat(customizationRecipeResults));
    }

    private static Result ValidateItemExistanceInSchematics(TempModelContext data)
    {
        var sources = GetClassNames(data.Items)
            .Concat(GetClassNames(data.Vehicles))
            .Concat(GetClassNames(data.Consumables))
            .Concat(GetClassNames(data.Resources))
            .Concat(GetClassNames(data.Ammunitions))
            .Concat(GetClassNames(data.Equipments))
            .Concat(GetClassNames(data.Weapons))
            .Concat(GetClassNames(data.Buildings))
            .Concat(GetClassNames(data.Miners))
            .Concat(GetClassNames(data.Generators))
            .Concat(GetClassNames(data.Emotes))
            .Concat(GetClassNames(data.Statues))
            .Concat(GetClassNames(data.Schematics))
            .Concat(GetClassNames(data.CustomizationRecipes))
            .Concat(GetClassNames(data.Recipes))
            .Concat(GetClassNames(data.Creatures))
            .Concat(GetClassNames(data.Plants))
            .ToArray();

        var results = new List<Result>();
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var schematic in data.Schematics)
        {
            var controlEntities = schematic.Costs.Select(p => p.ItemClassName)
                    .Concat(GetClassNames(schematic.GivesItems))
                    .Concat(GetClassNames(schematic.UnlocksScannerResourcePairs))
                    .Concat(GetClassNames(schematic.UnlocksScannerResources))
                    .Concat(GetClassNames(schematic.UnlocksEmotes))
                    .Concat(GetClassNames(schematic.UnlocksRecipes))
                    .Concat(schematic.Dependencies.Select(p => p.Schematics.Select(x => x.ClassName)).SelectMany(p => p))
                    .ToList();

            foreach (var scannerObject in schematic.UnlocksScannableObjects)
            {
                controlEntities.Add(scannerObject.ItemClassName + scannerObject.CreatureClassName + scannerObject.PlantClassName);
                controlEntities.AddRange(scannerObject.ScanningActors.Select(p => string.IsNullOrEmpty(p.ItemClassName) ? p.BuildingClassName! : p.ItemClassName));
            }

            controlEntities = controlEntities
                .Distinct()
                .ToList();

            var differences = controlEntities.Except(sources).ToList();
            if (!differences.Any())
                continue;

            results.Add(Result.Failure(
                $"Schematic: {schematic.Name} could not find the following references: {string.Join(Environment.NewLine, differences)}"));
        }

        return Result.Combine(results);
    }

    private static IEnumerable<string> GetClassNames<T>(IEnumerable<T> source) where T : IClassNamePrimaryKey =>
        source.Select(p => p.ClassName);
}
