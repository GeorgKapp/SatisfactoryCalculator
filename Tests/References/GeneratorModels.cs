namespace SatisfactoryCalculator.Tests.References;

internal class GeneratorModels
{
    public static GeneratorModel CoalGenerator = new GeneratorModel
    {
        ClassName = "GeneratorCoal_C",
        Name = "Coal Generator",
        Form = null,
        PowerProduction = 75,
        PowerProductionExponent = 1.3,
        SupplementalToPowerRatio = 10,
        SupplementalLoadAmount = 1000
    };
}
