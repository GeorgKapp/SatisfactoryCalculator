namespace SatisfactoryCalculator.Source.Features.Generator;

internal interface IGenerator : IBuilding
{
    decimal PowerProduction { get; }
    decimal? SupplementalToPowerRatio { get; }
    decimal? SupplementalLoadAmount { get; }
}