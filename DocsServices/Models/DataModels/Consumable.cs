#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Consumable : IBase
{
	public string ClassName { get; set; }
	public double? HealthGain { get; set; }
}
