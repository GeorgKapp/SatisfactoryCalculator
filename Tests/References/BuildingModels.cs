using SatisfactoryCalculator.Source.Features.Building;

namespace SatisfactoryCalculator.Tests.References;

internal static class BuildingModels
{
    public static readonly Building Constructor = new()
    {
        ClassName = "ConstructorMk1",
        Name = "Constructor",
        ManufactoringSpeed = 1,
        PowerConsumption = 4,
        PowerConsumptionExponent = 1.6M
    };

    public static readonly Building OilRefinery = new()
    {
        ClassName = "OilRefinery",
        Name = "Refinery",
        ManufactoringSpeed = 1,
        PowerConsumption = 30,
        PowerConsumptionExponent = 1.6M
    };
}