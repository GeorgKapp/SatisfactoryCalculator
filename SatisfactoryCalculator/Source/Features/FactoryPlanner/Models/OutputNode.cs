namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal class OutputNode
{
    public IBuilding ProductionBuilding { get; set; }
    public decimal ProductionBuildingAmount { get; set; }

    public IItem ProducedItem { get; set; }
    public decimal ProducedItemAmount { get; set; }
    
    public IRecipe Recipe { get; set; }
    
    public double Overclock { get; set; }

    public List<OutputNode> FactoryConfigurationOutputNodeDependencies { get; set; } = new();
}
