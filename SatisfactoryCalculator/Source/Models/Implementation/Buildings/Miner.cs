namespace SatisfactoryCalculator.Source.Models;

internal class Miner : IMiner
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public string Description { get;init; }
    public decimal? ManufactoringSpeed { get; init; }
    public decimal? PowerConsumption { get; init; }
    public decimal? PowerConsumptionExponent { get; init; }
    public PowerConsumptionRange? PowerConsumptionRange { get; init; }
    public IResource[] Resources { get; set; }
    public decimal ExtractCycleTime { get; set; }
}
