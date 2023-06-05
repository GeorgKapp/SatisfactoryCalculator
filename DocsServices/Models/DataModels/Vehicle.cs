// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Vehicle : IBase
{
	public string ClassName { get; set; }
	public double? FuelConsumption { get; set; }
	public int? InventorySize { get; set; }
}
