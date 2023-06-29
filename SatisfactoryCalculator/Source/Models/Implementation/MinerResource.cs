namespace SatisfactoryCalculator.Source.Models;

internal class MinerResource
{
    public IResource Resource { get; set; }
    public decimal? AmountPerMinute { get; set; }
    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
    public IMiner Miner { get; set; }
}
