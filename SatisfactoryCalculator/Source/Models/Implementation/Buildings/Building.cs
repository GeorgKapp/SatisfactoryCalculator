using SatisfactoryCalculator.Source.Models.Interfaces;

namespace SatisfactoryCalculator.Source.Models;

internal class Building : IBuilding
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Form? Form { get; set; }
    public double? ManufactoringSpeed { get; set; }
    public double? PowerConsumption { get; set; }
    public double? PowerConsumptionExponent { get; set; }
    public PowerConsumptionRange? PowerConsumptionRange { get; set; }
    public BitmapImage Image { get; set; }
}
