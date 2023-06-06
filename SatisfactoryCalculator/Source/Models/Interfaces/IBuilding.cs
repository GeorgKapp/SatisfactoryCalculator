// ReSharper disable UnusedMemberInSuper.Global
namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IBuilding : IEntity
{
    string Description { get; }
    double? ManufactoringSpeed { get; }
    double? PowerConsumption { get; }
    double? PowerConsumptionExponent { get; init; }
    PowerConsumptionRange? PowerConsumptionRange { get; init; }
}