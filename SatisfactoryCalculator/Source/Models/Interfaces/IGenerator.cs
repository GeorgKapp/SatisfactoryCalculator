namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IGenerator : IBuilding
{
    decimal PowerProduction { get; }
    decimal? SupplementalToPowerRatio { get; }
    decimal? SupplementalLoadAmount { get; }
}