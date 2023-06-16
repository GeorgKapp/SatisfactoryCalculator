namespace Data.Models.Implementation;

public class Miner : IClassNamePrimaryKey
{
    public string ClassName { get; set; }
    public Building Building { get; set; }
    
    public int ItemsPerCycle { get; set; }
    public decimal? ExtractCycleTime { get; set; }
    public Form AllowedResourceForm { get; set; }
}
