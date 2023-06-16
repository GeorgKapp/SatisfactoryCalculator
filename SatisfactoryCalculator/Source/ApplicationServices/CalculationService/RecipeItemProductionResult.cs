// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class RecipeItemProductionResult
{
    public decimal Amount { get; init; }
    public decimal AmountPerMinute { get; init; }
    public decimal PowerConsumption { get; init; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public decimal Time { get; set; }
}
