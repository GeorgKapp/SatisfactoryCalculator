#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Fuel
{
	public string FuelClass { get; set; }
	public string? SupplementalResourceClass { get; set; }
	public string? ByProduct { get; set; }
	public int? ByProductAmount { get; set; }
}