namespace SatisfactoryCalculator.Source.Models;

internal class Fuel
{
    public IGenerator Generator { get; set; }
    public FuelItem Ingredient { get; set; }
    public FuelItem? SupplementalIngredient { get; set; }
    public FuelItem? ByProduct { get; set; }
    public double? ByProductAmount { get; set; }
}