namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Building : IBase, IIcon
{
	public string ClassName { get; set; }
	public string DisplayName { get; set; }
	public string Description { get; set; }
	public string SmallIconPath { get; set; }
	public string BigIconPath { get; set; }
	public Form? Form { get; set; }
	public double? PowerConsumption { get; set; }
	public double? PowerConsumptionExponent { get; set; }
	public double? ManuFacturingSpeed { get; set; }
	public PowerConsumptionRange? PowerConsumptionRange { get; set; }
}
