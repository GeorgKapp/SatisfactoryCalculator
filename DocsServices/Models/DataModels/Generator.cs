#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Generator : IBase
{
	public string ClassName { get; set; }
	public double PowerProduction { get; init; }
	public double? SupplementToPowerRatio { get; init; }
	public double? SupplementalLoadAmount { get; init; }
	public Fuel[] Fuels { get; set; }
}
