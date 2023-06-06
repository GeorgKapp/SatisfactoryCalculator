// ReSharper disable CheckNamespace
namespace SatisfactoryCalculator.Source.Models;

internal class Generator : Building, IGenerator
{
    public double PowerProduction { get; init; }
    public double? SupplementalToPowerRatio { get; init; }
    public double? SupplementalLoadAmount { get; init; }
}
