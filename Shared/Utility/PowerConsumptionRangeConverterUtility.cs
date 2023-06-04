namespace SatisfactoryCalculator.Shared.Utility;

public static class PowerConsumptionRangeConverterUtility
{
	public static PowerConsumptionRange? ConverToPowerConsumption(double? estimatedMininumPowerConsumption, double? estimatedMaximumPowerConsumption)
	{
		return estimatedMininumPowerConsumption.HasValue
			? new PowerConsumptionRange(estimatedMininumPowerConsumption.Value,estimatedMaximumPowerConsumption!.Value)
			: null;
	}

	public static PowerConsumptionRange? ConverToPowerVariableConsumption(double variablePowerConsumptionConstant, double variablePowerConsumptionFactor)
	{
		return variablePowerConsumptionConstant != 0.0
			? new PowerConsumptionRange(variablePowerConsumptionConstant, variablePowerConsumptionConstant + variablePowerConsumptionFactor)
			: null;
	}
}
