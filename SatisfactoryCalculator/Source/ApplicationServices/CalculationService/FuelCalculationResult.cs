// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class FuelCalculationResult
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public decimal FuelBurnTime { get; set; }
    public decimal PowerProduction { get; set; }
    public decimal AmountPerMinute { get; set; }
    public decimal? SupplementalAmountPerMinute { get; set; }
    public decimal? ByProductAmountPerMinute { get; set; }
}

