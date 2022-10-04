namespace SatisfactoryCalculator.Source.Models;

public class RecipeBuildingModel
{
    public BuildingModel Building { get; set; }
    public string BuildingName { get; set; }
    public PowerConsumptionRange? PowerConsumptionRange { get; set; }
}
