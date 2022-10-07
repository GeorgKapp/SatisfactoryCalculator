namespace SatisfactoryCalculator.Source.Models;

internal class RecipeBuildingModel
{
    public BuildingModel Building { get; set; }
    public string BuildingName { get; set; }
    public PowerConsumptionRange? PowerConsumptionRange { get; set; }
}
