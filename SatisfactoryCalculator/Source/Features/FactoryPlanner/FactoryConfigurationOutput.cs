namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal class FactoryConfigurationOutput
{
    public int? ID { get; set; }
    public IEntity? Entity { get; set; }
    public decimal? Amount { get; set; }
    public decimal? BuildingAmount { get; set; }
    public IRecipe AlternateRecipe { get; set; }
}