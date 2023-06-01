namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class FuelCalculationResult
{
    public double FuelBurnTime { get; set; }
    public double PowerProduction { get; set; }
    public double AmountPerMinute { get; set; }
    public double? SupplementalAmountPerMinute { get; set; }
    public double? ByProductAmountPerMinute { get; set; }
}

