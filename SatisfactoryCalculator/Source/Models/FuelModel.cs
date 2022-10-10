namespace SatisfactoryCalculator.Source.Models;

internal class FuelModel
{
    public GeneratorModel Generator { get; set; }
    public FuelContentModel Ingredient { get; set; }
    public FuelContentModel? SupplementalIngredient { get; set; }
    public FuelContentModel? ByProduct { get; set; }
}