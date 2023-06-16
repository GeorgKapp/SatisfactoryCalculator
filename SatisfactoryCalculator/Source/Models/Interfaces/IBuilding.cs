// ReSharper disable UnusedMemberInSuper.Global
namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IBuilding : IEntity
{
    string Description { get; }
    decimal? ManufactoringSpeed { get; }
    decimal? PowerConsumption { get; }
    decimal? PowerConsumptionExponent { get; init; }
    PowerConsumptionRange? PowerConsumptionRange { get; init; }
}