namespace SatisfactoryCalculator.Source.Features.Building;

internal interface IBuilding : IEntity
{
    string Description { get; }
    decimal? ManufactoringSpeed { get; }
    decimal? PowerConsumption { get; }
    decimal? PowerConsumptionExponent { get; init; }
    PowerConsumptionRange? PowerConsumptionRange { get; init; }
}