namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private Result ValidateForDuplicates<T>(IEnumerable<T> source) where T : IBase
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

    private Result ValidateReferencesByTarget<T>(IEnumerable<T> source, IEnumerable<T> target) where T : IBase
    {
        var missingReferences = target.Select(p => p.ClassName)
            .Except(source.Select(p => p.ClassName))
            .ToArray();

        return missingReferences.Length > 0
            ? Result.Failure($"Reference Items missing: {string.Join(Environment.NewLine, missingReferences)}")
            : Result.Success();
    }

    private Result SeperatelyValidateDataForDuplicates(Data data)
    {
        return Result.Combine(
            new Result[] 
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

    private Result ValidateDataReferences(Data data)
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
        if (!buildingValidationResult.IsSuccess)
            return buildingValidationResult;

        return Result.Success();
    }

    private Result ValidateItemExistanceInRecipes(Data data)
    {
        string[] sources = GetClassNames(data.Items)
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

        var recipes = data.Recipes.Cast<IRecipe>()
            .Concat(data.CustomizationRecipes.Cast<IRecipe>())
            .ToArray();

        var results = new List<Result>();
        foreach (var recipe in recipes)
        {
            string[] recipeContentNames = GetClassNames(recipe.Ingredients)
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

    private Result ValidateItemExistanceInSchematics(Data data)
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

        var results = new List<Result>();
        foreach (Schematic schematic in data.Schematics)
        {
            var controlEntities =
                GetClassNames(schematic.Cost)
                .Concat(GetClassNames(schematic.ItemsToGive))
                .Concat(schematic.UnlocksScannerResourcePairs)
                .Concat(schematic.UnlocksScannerResources)
                .Concat(schematic.Emotes)
                .ToArray();

            var differences = controlEntities.Except(sources).ToList();
            if (!differences.Any())
                continue;

            results.Add(Result.Failure(
               $"Schematic: {schematic.DisplayName} could not find the following references: {string.Join(Environment.NewLine, differences)}"));
        }

        return Result.Combine(results);
    }

    private IEnumerable<string> GetClassNames<T>(IEnumerable<T> source) where T : IBase =>
        source.Select(p => p.ClassName);
}
