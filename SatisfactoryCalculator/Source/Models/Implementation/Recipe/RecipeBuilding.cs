// ReSharper disable CheckNamespace
namespace SatisfactoryCalculator.Source.Models;

internal class RecipeBuilding
{
    public RecipeBuilding(IBuilding building, PowerConsumptionRange? powerConsumptionRange)
    {
        Building = building;
        PowerConsumptionRange = powerConsumptionRange;
    }

    public RecipeBuilding(IBuilding building)
    {
        Building = building;
    }

    public IBuilding Building { get; }
    
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public PowerConsumptionRange? PowerConsumptionRange { get; set; }
}
