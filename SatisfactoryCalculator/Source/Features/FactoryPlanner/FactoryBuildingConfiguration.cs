namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal class FactoryBuildingConfiguration
{
    public int? ID { get; set; }
    
    public double Overclock { get; set; }
    
    public IBuilding Building { get; set; }
    public int BuildingAmount { get; set; }
    
    public IEntity ProducedEntity { get; set; }
    public decimal Amount { get; set; }
}
