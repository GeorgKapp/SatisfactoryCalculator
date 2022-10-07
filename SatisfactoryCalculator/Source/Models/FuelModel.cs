namespace SatisfactoryCalculator.Source.Models;

internal class FuelModel
{
    public GeneratorModel Generator { get; set; }
    public FuelContentModel[] Ingredients { get; set; }
    public FuelContentModel? ByProduct { get; set; }
}