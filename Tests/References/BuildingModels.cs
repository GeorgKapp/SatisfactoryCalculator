namespace SatisfactoryCalculator.Tests.References;

internal class BuildingModels
{
    public static Building Constructor = new Building
    {
        ClassName = "ConstructorMk1_C",
        Name = "Constructor",
        Form = null,
        ManufactoringSpeed = 1,
        PowerConsumption = 4,
        PowerConsumptionExponent = 1.6,
        PowerConsumptionRange = null
    };

    public static Building OilRefinery = new Building
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
