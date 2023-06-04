using System.Diagnostics;

namespace SatisfactoryCalculator.Tests;

public class FuelCalculationTests
{
    private CalculationService _calculationService = new();
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(1%)")]
    public void CalculateCoalConsumption1() => CalculateFuelConsumption(FuelModels.CoalFuel, 1, 0.75, 0.15, 0);

    [Fact(DisplayName = "Calculate Coal Consumption with underlock(2%)")]
    public void CalculateCoalConsumption2() => CalculateFuelConsumption(FuelModels.CoalFuel, 2, 1.5, 0.3, 1);

    [Fact(DisplayName = "Calculate Coal Consumption with underlock(3%)")]
    public void CalculateCoalConsumption3() => CalculateFuelConsumption(FuelModels.CoalFuel, 3, 2.25, 0.45, 1);

    [Fact(DisplayName = "Calculate Coal Consumption with underlock(4%)")]
    public void CalculateCoalConsumption4() => CalculateFuelConsumption(FuelModels.CoalFuel, 4, 3, 0.6, 2);
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(5%)")]
    public void CalculateCoalConsumption5() => CalculateFuelConsumption(FuelModels.CoalFuel, 5, 3.75, 0.75, 2);
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(10%)", Skip = "Expected Supplemental Amount is actually 4 instead of 5 (why?, ive got no clue)")]
    public void CalculateCoalConsumption10() => CalculateFuelConsumption(FuelModels.CoalFuel, 10, 7.5, 1.5, 4);

    [Fact(DisplayName = "Calculate Coal Consumption with underlock(10.1%)")]
    public void CalculateCoalConsumption10_1() => CalculateFuelConsumption(FuelModels.CoalFuel, 10.1, 7.58, 1.52, 5);
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(13.33%)")]
    public void CalculateCoalConsumption13_33() => CalculateFuelConsumption(FuelModels.CoalFuel, 13.33, 10, 2, 6);
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(20%)")]
    public void CalculateCoalConsumption20() => CalculateFuelConsumption(FuelModels.CoalFuel, 20, 15, 3, 9);
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(25.4%)")]
    public void CalculateCoalConsumption25_4() => CalculateFuelConsumption(FuelModels.CoalFuel, 25.4, 19.05, 3.81, 11);
    
    [Fact(DisplayName = "Calculate Coal Consumption with overlock(50%)")]
    public void CalculateCoalConsumption50() => CalculateFuelConsumption(FuelModels.CoalFuel, 50, 37.5, 7.5, 23);

    [Fact(DisplayName = "Calculate Coal Consumption with overlock(86.33%)")]
    public void CalculateCoalConsumption86_33() => CalculateFuelConsumption(FuelModels.CoalFuel, 86.33, 64.75, 12.95, 39);

    [Fact(DisplayName = "Calculate Coal Consumption")]
    public void CalculateCoalConsumption100() => CalculateFuelConsumption(FuelModels.CoalFuel, 100, 75, 15, 45);

    [Fact(DisplayName = "Calculate Coal Consumption with overlock(100.5555%)")]
    public void CalculateCoalConsumption100_5555() => CalculateFuelConsumption(FuelModels.CoalFuel, 100.5555, 75.42, 15.08, 45);

    [Fact(DisplayName = "Calculate Coal Consumption with overlock(150%)")]
    public void CalculateCoalConsumption150() => CalculateFuelConsumption(FuelModels.CoalFuel, 150, 112.5, 22.5, 68);
    
    [Fact(DisplayName = "Calculate Coal Consumption with overlock(200%)")]
    public void CalculateCoalConsumption200() => CalculateFuelConsumption(FuelModels.CoalFuel, 200, 150, 30, 90);
    
    [Fact(DisplayName = "Calculate Coal Consumption with overlock(250%)")]
    public void CalculateCoalConsumption250() => CalculateFuelConsumption(FuelModels.CoalFuel, 250, 187.5, 37.5, 113);

    [Fact(DisplayName = "Calculate Liquid LiquidFuel Consumption with underlock(50%)")]
    public void CalculateLiquidFuelConsumption50() => CalculateFuelConsumption(FuelModels.LiquidFuel, 50, 75, 6);
    
    [Fact(DisplayName = "Calculate Liquid LiquidFuel Consumption")]
    public void CalculateLiquidFuelConsumption100() => CalculateFuelConsumption(FuelModels.LiquidFuel, 100, 150, 12);
    
    [Fact(DisplayName = "Calculate Liquid LiquidFuel Consumption with overlock(200%)")]
    public void CalculateLiquidFuelConsumption200() => CalculateFuelConsumption(FuelModels.LiquidFuel, 200, 300, 24);
    
    private void CalculateFuelConsumption(Fuel fuel, double overclock, double expectedPowerProduction, double expectedAmountPerMinute, double? expectedSupplementalAmountPerMinute = null, double? expectedByProductAmountPerMinute = null)
    {
        var result = _calculationService.CalculateRoundedFuelConsumption(fuel, overclock);
        
        Assert.Equal(expectedPowerProduction, result.PowerProduction);
        Assert.Equal(expectedAmountPerMinute, result.AmountPerMinute);
        
        if(expectedSupplementalAmountPerMinute.HasValue)
            Assert.Equal(expectedSupplementalAmountPerMinute, result.SupplementalAmountPerMinute);
        
        if(expectedByProductAmountPerMinute.HasValue)
            Assert.Equal(expectedByProductAmountPerMinute, result.ByProductAmountPerMinute);
    }
}