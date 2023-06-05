namespace SatisfactoryCalculator.Tests.References;

internal static class BuildingModels
{
    public static readonly Building Constructor = new()
    {
        ClassName = "ConstructorMk1_C",
        Name = "Constructor",
        Form = null,
        ManufactoringSpeed = 1,
        PowerConsumption = 4,
        PowerConsumptionExponent = 1.6,
        PowerConsumptionRange = null
    };

    public static readonly Building OilRefinery = new()
    {
        ClassName = "OilRefinery_C",
        Name = "Refinery",
        Form = null,
        ManufactoringSpeed = 1,
        PowerConsumption = 30,
        PowerConsumptionExponent = 1.6,
        PowerConsumptionRange = null
    };
}
