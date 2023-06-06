// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class RecipeItemProductionResult
{
    public double Amount { get; init; }
    public double AmountPerMinute { get; init; }
    public double PowerConsumption { get; init; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public double Time { get; set; }
}
