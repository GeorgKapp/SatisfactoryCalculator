// ReSharper disable HeapView.ObjectAllocation
namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private static Result ValidateForDuplicates<T>(IEnumerable<T> source) where T : IBase
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

    private static Result ValidateReferencesByTarget<T>(IEnumerable<T> source, IEnumerable<T> target) where T : IBase
    {
        var missingReferences = target.Select(p => p.ClassName)
            .Except(source.Select(p => p.ClassName))
            .ToArray();

        return missingReferences.Length > 0
            ? Result.Failure($"Reference Items missing: {string.Join(Environment.NewLine, missingReferences)}")
            : Result.Success();
    }

    private static Result SeperatelyValidateDataForDuplicates(Data data)
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
                ValidateForDuplicates(data.Ammunition),
                ValidateForDuplicates(data.Weapons),
                ValidateForDuplicates(data.Equipments),
                ValidateForDuplicates(data.Generators),
                ValidateForDuplicates(data.Emotes),
                ValidateForDuplicates(data.Statues) }
            );
    }

    private static Result ValidateDataReferences(Data data)
    {
        var items = data.Items.Cast<IBase>();

        var itemReferences = data.Consumables.Cast<IBase>()
            .Concat(data.Equipments)
            .Concat(data.Weapons)
            .Concat(data.Vehicles);

        var itemValidationResult = ValidateReferencesByTarget(items, itemReferences);
        if (!itemValidationResult.IsSuccess)
            return itemValidationResult;

        var buildings = data.Buildings.Cast<IBase>();

        var buildingReferences = data.Generators.Cast<IBase>()
            .Concat(data.Miners);

        var buildingValidationResult = ValidateReferencesByTarget(buildings, buildingReferences);
        
        return !buildingValidationResult.IsSuccess 
            ? buildingValidationResult 
            : Result.Success();
    }

    private static Result ValidateItemExistanceInRecipes(Data data)
    {
        var sources = GetClassNames(data.Items)
            .Concat(GetClassNames(data.Vehicles))
            .Concat(GetClassNames(data.Consumables))
            .Concat(GetClassNames(data.Resources))
            .Concat(GetClassNames(data.Ammunition))
            .Concat(GetClassNames(data.Equipments))
            .Concat(GetClassNames(data.Weapons))
            .Concat(GetClassNames(data.Buildings))
            .Concat(GetClassNames(data.Miners))
            .Concat(GetClassNames(data.Generators))
            .Concat(GetClassNames(data.Emotes))
            .Concat(GetClassNames(data.Statues))
            .ToArray();

        var recipes = data.Recipes
            .Concat(data.CustomizationRecipes.Cast<IRecipe>())
            .ToArray();

        var results = new List<Result>();
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var recipe in recipes)
        {
            var recipeContentNames = GetClassNames(recipe.Ingredients)
                .Concat(GetClassNames(recipe.Products))
                .ToArray();

            var differences = recipeContentNames.Except(sources).ToList();
            if (!differences.Any())
                continue;

            results.Add(Result.Failure(
                $"Recipe: {recipe.DisplayName} could not find the following references: {string.Join(Environment.NewLine, differences)}"));
        }
        return Result.Combine(results);
    }

    private static Result ValidateItemExistanceInSchematics(Data data)
    {
        var sources = GetClassNames(data.Items)
            .Concat(GetClassNames(data.Vehicles))
            .Concat(GetClassNames(data.Consumables))
            .Concat(GetClassNames(data.Resources))
            .Concat(GetClassNames(data.Ammunition))
            .Concat(GetClassNames(data.Equipments))
            .Concat(GetClassNames(data.Weapons))
            .Concat(GetClassNames(data.Buildings))
            .Concat(GetClassNames(data.Miners))
            .Concat(GetClassNames(data.Generators))
            .Concat(GetClassNames(data.Emotes))
            .Concat(GetClassNames(data.Statues))
            .Concat(GetClassNames(data.Schematics))
            .ToArray();

        var results = new List<Result>();
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var schematic in data.Schematics)
        {
            var controlEntities =
                GetClassNames(schematic.Cost)
                    .Concat(GetClassNames(schematic.ItemsToGive))
                    .Concat(schematic.UnlocksScannerResourcePairs)
                    .Concat(schematic.UnlocksScannerResources)
                    .Concat(schematic.Emotes)
                    .Concat(schematic.UnlocksRecipes)
                    .Concat(schematic.SchematicDependencies.SelectMany(p => p.Schematics))
                    .ToList();

            if (schematic.UnlocksScannerObject is not null)
            {
                controlEntities.Add(schematic.UnlocksScannerObject.ItemClass);
                controlEntities.AddRange(schematic.UnlocksScannerObject.ActorsAllowedToScan);
            }

            var differences = controlEntities.Except(sources).ToList();
            if (!differences.Any())
                continue;

            results.Add(Result.Failure(
                $"Schematic: {schematic.DisplayName} could not find the following references: {string.Join(Environment.NewLine, differences)}"));
        }

        return Result.Combine(results);
    }

    private static IEnumerable<string> GetClassNames<T>(IEnumerable<T> source) where T : IBase =>
        source.Select(p => p.ClassName);
}
