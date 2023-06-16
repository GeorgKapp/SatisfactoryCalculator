namespace SatisfactoryCalculator.Shared.Utility;

public static class PowerConsumptionRangeConverterUtility
{
	public static PowerConsumptionRange? ConverToPowerConsumption(decimal? estimatedMininumPowerConsumption, decimal? estimatedMaximumPowerConsumption)
	{
		return estimatedMininumPowerConsumption.HasValue
			? new PowerConsumptionRange(estimatedMininumPowerConsumption.Value,estimatedMaximumPowerConsumption!.Value)
			: null;
	}

	public static PowerConsumptionRange? ConverToPowerVariableConsumption(decimal variablePowerConsumptionConstant, decimal variablePowerConsumptionFactor)
	{
		return variablePowerConsumptionConstant != (decimal)0.0
			? new PowerConsumptionRange(variablePowerConsumptionConstant, variablePowerConsumptionConstant + variablePowerConsumptionFactor)
			: null;
	}
}
