namespace SatisfactoryCalculator.Shared.Models;

public class PowerConsumptionRange
{
	public double StartPowerConsumption { get; set; }
	public double EndPowerConsumption { get; set; }

	public PowerConsumptionRange() { }
	public PowerConsumptionRange(double startPowerConsumption, double endPowerConsumption)
	{
		StartPowerConsumption = startPowerConsumption;
		EndPowerConsumption = endPowerConsumption;
	}
}
