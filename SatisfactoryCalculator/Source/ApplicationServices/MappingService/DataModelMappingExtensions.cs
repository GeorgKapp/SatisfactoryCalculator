// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

internal static class DataModelMappingExtensions
{
    public static IRecipe[] OrderRecipeModel(this IEnumerable<IRecipe> input) => input
            .OrderBy(p => p.IsAlternateRecipe)
            .ThenBy(p => p.Name)
            .ToArray();
}
