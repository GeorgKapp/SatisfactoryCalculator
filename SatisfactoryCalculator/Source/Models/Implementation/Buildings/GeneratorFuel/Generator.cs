// ReSharper disable CheckNamespace
namespace SatisfactoryCalculator.Source.Models;

internal class Generator : Building, IGenerator
{
    public decimal PowerProduction { get; init; }
    public decimal? SupplementalToPowerRatio { get; init; }
    public decimal? SupplementalLoadAmount { get; init; }
}
