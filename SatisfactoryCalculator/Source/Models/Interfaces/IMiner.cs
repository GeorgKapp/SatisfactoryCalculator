namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IMiner : IBuilding
{
    public decimal ExtractCycleTime { get; set; }
}
