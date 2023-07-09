namespace SatisfactoryCalculator.Source.Features.Building;

internal class Building : IBuilding
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal? ManufactoringSpeed { get; init; }
    public decimal? PowerConsumption { get; init; }
    public decimal? PowerConsumptionExponent { get; init; }
    public PowerConsumptionRange? PowerConsumptionRange { get; init; }
    public BitmapImage? Image { get; init; }
}
