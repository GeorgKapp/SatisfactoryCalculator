// ReSharper disable once CheckNamespace

using Recipe = SatisfactoryCalculator.Source.Models.Recipe;

namespace SatisfactoryCalculator.Source.ApplicationServices;

internal static class DataModelMappingExtensions
{
    public static Recipe[] OrderRecipeModel(this IEnumerable<Recipe> input) => input
            .OrderBy(p => p.IsAlternateRecipe)
            .ThenBy(p => p.Name)
            .ToArray();
}
