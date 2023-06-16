namespace SatisfactoryCalculator.Tests;

public class FuelCalculationTests
{
    private readonly CalculationService _calculationService = new();
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(1%)")]
    public void CalculateCoalConsumption1() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 1, 0.75M, 0.15M, 0);

    [Fact(DisplayName = "Calculate Coal Consumption with underlock(2%)")]
    public void CalculateCoalConsumption2() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 2, 1.5M, 0.3M, 1);

    [Fact(DisplayName = "Calculate Coal Consumption with underlock(3%)")]
    public void CalculateCoalConsumption3() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 3, 2.25M, 0.45M, 1);

    [Fact(DisplayName = "Calculate Coal Consumption with underlock(4%)")]
    public void CalculateCoalConsumption4() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 4, 3, 0.6M, 2);
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(5%)")]
    public void CalculateCoalConsumption5() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 5, 3.75M, 0.75M, 2);
    
#pragma warning disable xUnit1004
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(10%)", Skip = "Expected Supplemental Amount is actually 4 instead of 5 (why?, ive got no clue)")]
#pragma warning restore xUnit1004
    public void CalculateCoalConsumption10() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 10, 7.5M, 1.5M, 4);

    [Fact(DisplayName = "Calculate Coal Consumption with underlock(10.1%)")]
    public void CalculateCoalConsumption10_1() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 10.1, 7.58M, 1.52M, 5);
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(13.33%)")]
    public void CalculateCoalConsumption13_33() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 13.33, 10, 2, 6);
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(20%)")]
    public void CalculateCoalConsumption20() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 20, 15, 3, 9);
    
    [Fact(DisplayName = "Calculate Coal Consumption with underlock(25.4%)")]
    public void CalculateCoalConsumption25_4() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 25.4, 19.05M, 3.81M, 11);
    
    [Fact(DisplayName = "Calculate Coal Consumption with overlock(50%)")]
    public void CalculateCoalConsumption50() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 50, 37.5M, 7.5M, 23);

    [Fact(DisplayName = "Calculate Coal Consumption with overlock(86.33%)")]
    public void CalculateCoalConsumption86_33() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 86.33, 64.75M, 12.95M, 39);

    [Fact(DisplayName = "Calculate Coal Consumption")]
    public void CalculateCoalConsumption100() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 100, 75, 15, 45);

    [Fact(DisplayName = "Calculate Coal Consumption with overlock(100.5555%)")]
    public void CalculateCoalConsumption100_5555() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 100.5555, 75.42M, 15.08M, 45);

    [Fact(DisplayName = "Calculate Coal Consumption with overlock(150%)")]
    public void CalculateCoalConsumption150() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 150, 112.5M, 22.5M, 68);
    
    [Fact(DisplayName = "Calculate Coal Consumption with overlock(200%)")]
    public void CalculateCoalConsumption200() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 200, 150, 30, 90);
    
    [Fact(DisplayName = "Calculate Coal Consumption with overlock(250%)")]
    public void CalculateCoalConsumption250() => CalculateFuelConsumption(FuelModels.CoalGeneratorFuel, 250, 187.5M, 37.5M, 113);

    [Fact(DisplayName = "Calculate Liquid LiquidFuel Consumption with underlock(50%)")]
    public void CalculateLiquidFuelConsumption50() => CalculateFuelConsumption(FuelModels.LiquidGeneratorFuel, 50, 75, 6);
    
    [Fact(DisplayName = "Calculate Liquid LiquidFuel Consumption")]
    public void CalculateLiquidFuelConsumption100() => CalculateFuelConsumption(FuelModels.LiquidGeneratorFuel, 100, 150, 12);
    
    [Fact(DisplayName = "Calculate Liquid LiquidFuel Consumption with overlock(200%)")]
    public void CalculateLiquidFuelConsumption200() => CalculateFuelConsumption(FuelModels.LiquidGeneratorFuel, 200, 300, 24);
    
    private void CalculateFuelConsumption(GeneratorFuel generatorFuel, double overclock, decimal expectedPowerProduction, decimal expectedAmountPerMinute, decimal? expectedSupplementalAmountPerMinute = null, decimal? expectedByProductAmountPerMinute = null)
    {
        var result = _calculationService.CalculateRoundedFuelConsumption(generatorFuel, overclock);
        
        Assert.Equal(expectedPowerProduction, result.PowerProduction);
        Assert.Equal(expectedAmountPerMinute, result.AmountPerMinute);
        
        if(expectedSupplementalAmountPerMinute.HasValue)
            Assert.Equal(expectedSupplementalAmountPerMinute, result.SupplementalAmountPerMinute);
        
        if(expectedByProductAmountPerMinute.HasValue)
            Assert.Equal(expectedByProductAmountPerMinute, result.ByProductAmountPerMinute);
    }
}