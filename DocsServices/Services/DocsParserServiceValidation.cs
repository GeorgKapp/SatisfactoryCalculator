namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private bool ValidateForDuplicates<T>(IEnumerable<T> source, IExtendedProgress<string> progress) where T : IBase
    {
        var duplicates = source
            .GroupBy(p => p.ClassName)
            .Where(p => p.Count() > 1)
            .Select(p => p.Key)
            .ToArray();

        if (duplicates.Length > 0)
        {
            foreach (string item in duplicates)
                progress.ReportErrorMessage("Duplicate source Item in type: " + typeof(T).Name + " detected: " + item);

            return false;
        }
        return true;
    }

    private bool ValidateReferencesByTarget<T>(IEnumerable<T> source, IEnumerable<T> target, IExtendedProgress<string> progress) where T : IBase
    {
        var missingReferences = target.Select(p => p.ClassName)
            .Except(source.Select(p => p.ClassName))
            .ToArray();

        if (missingReferences.Length > 0)
        {
            foreach (string item in missingReferences)
                progress.ReportErrorMessage("Reference Item missing: " + item);

            return false;
        }
        return true;
    }

    private bool SeperatelyValidateDataForDuplicates(Data data, IExtendedProgress<string> progress)
    {
        if (!ValidateForDuplicates(data.Items, progress) ||
            !ValidateForDuplicates(data.Recipes, progress) ||
            !ValidateForDuplicates(data.CustomizationRecipes, progress) ||
            !ValidateForDuplicates(data.Buildings, progress) ||
            !ValidateForDuplicates(data.Vehicles, progress) ||
            !ValidateForDuplicates(data.Miners, progress) ||
            !ValidateForDuplicates(data.Schematics, progress) ||
            !ValidateForDuplicates(data.Resources, progress) ||
            !ValidateForDuplicates(data.Consumables, progress) ||
            !ValidateForDuplicates(data.Ammunition, progress) ||
            !ValidateForDuplicates(data.Weapons, progress) ||
            !ValidateForDuplicates(data.Equipments, progress) ||
            !ValidateForDuplicates(data.Generators, progress) ||
            !ValidateForDuplicates(data.Emotes, progress) ||
            !ValidateForDuplicates(data.Statues, progress))

            return false;

        return true;
    }

    private bool ValidateDataReferences(Data data, IExtendedProgress<string> progress)
    {
        var items = data.Items.Cast<IBase>();

        var itemReferences = data.Consumables.Cast<IBase>()
            .Concat(data.Equipments.Cast<IBase>())
            .Concat(data.Weapons.Cast<IBase>())
            .Concat(data.Vehicles.Cast<IBase>());

        if (!ValidateReferencesByTarget(items, itemReferences, progress))
            return false;

        var buildings = data.Buildings.Cast<IBase>();

        var buildingReferences = data.Generators.Cast<IBase>()
            .Concat(data.Miners.Cast<IBase>());

        if (!ValidateReferencesByTarget(buildings, buildingReferences, progress))
            return false;

        return true;
    }

    private bool ValidateItemExistanceInRecipes(Data data, IExtendedProgress<string> progress)
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

        bool result = true;
        var recipes = data.Recipes.Cast<IRecipe>()
            .Concat(data.CustomizationRecipes.Cast<IRecipe>())
            .ToArray();

        foreach (var recipe in recipes)
        {
            string[] recipeContentNames = GetClassNames(recipe.Ingredients)
                .Concat(GetClassNames(recipe.Products))
                .ToArray();

            var differences = recipeContentNames.Except(sources).ToList();
            if (!differences.Any())
                continue;

            progress.ReportErrorMessage("Recipe: " + recipe.DisplayName + " could not find the following references:");
            foreach (string item in differences)
                progress.ReportErrorMessage(item);

            result = false;
        }
        return result;
    }

    private bool ValidateItemExistanceInSchematics(Data data, IExtendedProgress<string> progress)
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

        bool result = true;
        foreach (Schematic schematic in data.Schematics)
        {
            var controlEntities =
                GetClassNames(schematic.Cost)
                .Concat(GetClassNames(schematic.ItemsToGive))
                .Concat(schematic.UnlocksScannerResourcePairs)
                .Concat(schematic.UnlocksScannerResources)
                .Concat(schematic.Emotes)
                .ToArray();

            var difference = controlEntities.Except(sources).ToList();
            if (!difference.Any())
                continue;

            progress.ReportErrorMessage("Schematic: " + schematic.DisplayName + " could not find the following references:");
            foreach (string item in difference)
                progress.ReportErrorMessage(item);

            result = false;
        }
        return result;
    }

    private IEnumerable<string> GetClassNames<T>(IEnumerable<T> source) where T : IBase =>
        source.Select(p => p.ClassName);
}
