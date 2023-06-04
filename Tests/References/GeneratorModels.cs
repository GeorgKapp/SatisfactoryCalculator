namespace SatisfactoryCalculator.Tests.References;

internal class GeneratorModels
{
    public static Generator CoalGenerator = new Generator
    {
        ClassName = "GeneratorCoal_C",
        Name = "Coal Generator",
        PowerProduction = 75,
        SupplementalToPowerRatio = 10,
        SupplementalLoadAmount = 1000
    };
    
    public static Generator FuelGenerator = new Generator
    {
        ClassName = "GeneratorFuel_C",
        Name = "LiquidFuel Generator",
        PowerProduction = 150,
    };
    
    public static Generator NuclearGenerator = new Generator
    {
        ClassName = "GeneratorNuclear_C",
        Name = "Nuclear Generator",
        PowerProduction = 2500,
        SupplementalToPowerRatio = 2,
        SupplementalLoadAmount = 10000
    };
}
