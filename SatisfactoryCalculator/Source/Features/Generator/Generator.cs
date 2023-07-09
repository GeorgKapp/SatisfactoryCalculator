namespace SatisfactoryCalculator.Source.Features.Generator;

internal class Generator : Building.Building, IGenerator
{
    public decimal PowerProduction { get; init; }
    public decimal? SupplementalToPowerRatio { get; init; }
    public decimal? SupplementalLoadAmount { get; init; }
}
