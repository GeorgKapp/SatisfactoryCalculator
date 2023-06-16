// ReSharper disable UnusedMember.Global
namespace SatisfactoryCalculator.Tests.References;

internal static class GeneratorModels
{
    public static readonly Generator CoalGenerator = new()
    {
        ClassName = "GeneratorCoal",
        Name = "Coal Generator",
        PowerProduction = 75,
        SupplementalToPowerRatio = 10,
        SupplementalLoadAmount = 1000
    };
    
    public static readonly Generator FuelGenerator = new()
    {
        ClassName = "GeneratorFuel",
        Name = "LiquidFuel Generator",
        PowerProduction = 150
    };
    
    public static readonly Generator NuclearGenerator = new()
    {
        ClassName = "GeneratorNuclear",
        Name = "Nuclear Generator",
        PowerProduction = 2500,
        SupplementalToPowerRatio = 2,
        SupplementalLoadAmount = 10000
    };
}
