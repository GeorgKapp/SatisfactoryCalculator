#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Generator : IBase
{
	public string ClassName { get; set; }
	public double PowerProduction { get; set; }
	public double? SupplementToPowerRatio { get; set; }
	public double? SupplementalLoadAmount { get; set; }
	public Fuel[] Fuels { get; set; }
}
