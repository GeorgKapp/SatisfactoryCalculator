namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IBuilding : IEntity
{
    string Description { get; set; }
    Form? Form { get; set; }
    double? ManufactoringSpeed { get; set; }
    double? PowerConsumption { get; set; }
    double? PowerConsumptionExponent { get; set; }
    PowerConsumptionRange? PowerConsumptionRange { get; set; }
}