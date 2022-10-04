namespace SatisfactoryCalculator.Shared.Utility;

public static class PowerConsumptionRangeConverterUtility
{
	public static PowerConsumptionRange? ConverToPowerConsumption(double? estimatedMininumPowerConsumption, double? estimatedMaximumPowerConsumption)
	{
		return (!estimatedMininumPowerConsumption.HasValue) ? null : new PowerConsumptionRange
		{
			StartPowerConsumption = estimatedMininumPowerConsumption.Value,
			EndPowerConsumption = estimatedMaximumPowerConsumption.Value
		};
	}

	public static PowerConsumptionRange? ConverToPowerVariableConsumption(double variablePowerConsumptionConstant, double variablePowerConsumptionFactor)
	{
		if (variablePowerConsumptionConstant == 0.0)
		{
			return null;
		}
		return new PowerConsumptionRange
		{
			StartPowerConsumption = variablePowerConsumptionConstant,
			EndPowerConsumption = variablePowerConsumptionConstant + variablePowerConsumptionFactor
		};
	}
}
