namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IGenerator : IBuilding
{
    double PowerProduction { get; }
    double? SupplementalToPowerRatio { get; }
    double? SupplementalLoadAmount { get; }
}