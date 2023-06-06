// ReSharper disable CheckNamespace
#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models;

internal class Building : IBuilding
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public double? ManufactoringSpeed { get; init; }
    public double? PowerConsumption { get; init; }
    public double? PowerConsumptionExponent { get; init; }
    public PowerConsumptionRange? PowerConsumptionRange { get; init; }
    public BitmapImage? Image { get; init; }
}
