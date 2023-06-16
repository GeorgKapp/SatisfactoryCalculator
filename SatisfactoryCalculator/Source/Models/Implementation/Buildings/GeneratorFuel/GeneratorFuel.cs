// ReSharper disable CheckNamespace
#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models;

internal class GeneratorFuel
{
    public IGenerator Generator { get; init; }
    public FuelItem Ingredient { get; init; }
    public FuelItem? SupplementalIngredient { get; init; }
    public FuelItem? ByProduct { get; init; }
    public decimal? ByProductAmount { get; init; }
}