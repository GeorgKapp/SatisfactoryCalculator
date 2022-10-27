namespace SatisfactoryCalculator.Source.ApplicationServices;

internal static class DataModelMappingExtensions
{
    public static RecipeModel[] OrderRecipeModel(this IEnumerable<RecipeModel> input) => input
            .OrderBy(p => p.IsAlternateRecipe)
            .ThenBy(p => p.RecipeName)
            .ToArray();
}
