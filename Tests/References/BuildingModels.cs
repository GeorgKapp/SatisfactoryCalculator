namespace SatisfactoryCalculator.Tests.References;

internal class BuildingModels
{
    public static BuildingModel Constructor = new BuildingModel
    {
        ClassName = "ConstructorMk1_C",
        Name = "Constructor",
        Form = null,
        ManufactoringSpeed = 1,
        PowerConsumption = 4,
        PowerConsumptionExponent = 1.6,
        PowerConsumptionRange = null
    };

    public static BuildingModel OilRefinery = new BuildingModel
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
