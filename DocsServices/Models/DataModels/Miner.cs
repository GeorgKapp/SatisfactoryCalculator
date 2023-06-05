#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Miner : IBase
{
	public string ClassName { get; set; }
	public Form[] AllowedResourceForms { get; set; }
	public int ItemsPerCycle { get; set; }
	public double? ExtractCycleTime { get; set; }
}
