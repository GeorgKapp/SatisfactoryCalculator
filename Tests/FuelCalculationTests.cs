namespace SatisfactoryCalculator.Tests;

public class FuelCalculationTests
{
    private CalculationService _calculationService = new();
    
    [Fact(DisplayName = "Calculate Coal Consumption")]
    public void CalculateCoalConsumption() => CalculateFuelConsumption(FuelModels.CoalFuel, 100, 15);

    [Fact(DisplayName = "Calculate Coal Consumption with overlock(200%)")]
    public void CalculateCoalConsumptionOverclock() => CalculateFuelConsumption(FuelModels.CoalFuel, 200, 25.565411892857238);

    [Fact(DisplayName = "Calculate Liquid LiquidFuel Consumption")]
    public void CalculateLiquidFuelConsumptionOverclock() => CalculateFuelConsumption(FuelModels.LiquidFuel, 100, 12);
    
    
    private void CalculateFuelConsumption(FuelModel fuelModel, double overclock, double expectedAmountPerMinute, double? expectedSupplementalAmountPerMinute = null, double? expectedByProductAmountPerMinute = null)
    {
        var result = _calculationService.CalculateFuelConsumption(fuelModel, overclock);
        Assert.Equal(expectedAmountPerMinute, result.AmountPerMinute);
        
        if(expectedSupplementalAmountPerMinute.HasValue)
            Assert.Equal(expectedSupplementalAmountPerMinute, result.SupplementalAmountPerMinute);
        
        if(expectedByProductAmountPerMinute.HasValue)
            Assert.Equal(expectedByProductAmountPerMinute, result.ByProductAmountPerMinute);
    }
}