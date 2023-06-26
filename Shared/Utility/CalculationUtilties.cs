namespace SatisfactoryCalculator.Shared.Utility;

public static class CalculationUtilties
{
    public static decimal GetManufactoringBuildingSpeed(this decimal? manufactoringSpeed) => manufactoringSpeed ?? 1;
    public static double GetOverClockMultiplier(this double overclock) => overclock / DefaultPercentage;
    public static bool IsLiquidOrGas(this Form? form) => form is Form.Liquid or Form.Gas;
    public static bool IsLiquidOrGas(this Form form) => form is Form.Liquid or Form.Gas;
    public static decimal NormalizeAmount(this Form? form, decimal sourceInput) => form.IsLiquidOrGas() ? sourceInput / 1000 : sourceInput;
    public static decimal NormalizeAmount(this Form form, decimal sourceInput) => form.IsLiquidOrGas() ? sourceInput / 1000 : sourceInput;

    public const double DefaultPercentage = 100;
    public const decimal SecondsPerMinute = 60;
    public const double PowerConsumptionExponent = 1.321928;
}
