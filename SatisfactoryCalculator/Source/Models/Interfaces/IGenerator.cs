namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IGenerator : IBuilding
{
    double PowerProduction { get; set; }
    double? SupplementalToPowerRatio { get; set; }
    double? SupplementalLoadAmount { get; set; }
}