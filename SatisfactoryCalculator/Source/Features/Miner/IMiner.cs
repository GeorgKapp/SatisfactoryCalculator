namespace SatisfactoryCalculator.Source.Features.Miner;

internal interface IMiner : IBuilding
{
    public decimal ExtractCycleTime { get; set; }
}
