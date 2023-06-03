namespace SatisfactoryCalculator.Tests.References;

internal class GeneratorModels
{
    public static GeneratorModel CoalGenerator = new GeneratorModel
    {
        ClassName = "GeneratorCoal_C",
        Name = "Coal Generator",
        PowerProduction = 75,
        SupplementalToPowerRatio = 10,
        SupplementalLoadAmount = 1000
    };
    
    public static GeneratorModel FuelGenerator = new GeneratorModel
    {
        ClassName = "GeneratorFuel_C",
        Name = "LiquidFuel Generator",
        PowerProduction = 150,
    };
    
    public static GeneratorModel NuclearGenerator = new GeneratorModel
    {
        ClassName = "GeneratorNuclear_C",
        Name = "Nuclear Generator",
        PowerProduction = 2500,
        SupplementalToPowerRatio = 2,
        SupplementalLoadAmount = 10000
    };
}
