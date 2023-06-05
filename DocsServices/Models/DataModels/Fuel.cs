#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Fuel
{
	public string FuelClass { get; init; }
	public string? SupplementalResourceClass { get; init; }
	public string? ByProduct { get; init; }
	public int? ByProductAmount { get; init; }
}