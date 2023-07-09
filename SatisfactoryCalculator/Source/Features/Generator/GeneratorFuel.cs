namespace SatisfactoryCalculator.Source.Features.Generator;

internal class GeneratorFuel
{
    public IGenerator Generator { get; init; }
    public FuelItem Ingredient { get; init; }
    public FuelItem? SupplementalIngredient { get; init; }
    public FuelItem? ByProduct { get; init; }
    public decimal? ByProductAmount { get; init; }
}