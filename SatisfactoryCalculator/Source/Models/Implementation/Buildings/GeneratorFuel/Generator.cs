namespace SatisfactoryCalculator.Source.Models;

internal class Generator : Building, IGenerator
{
    public double PowerProduction { get; set; }
    public double? SupplementalToPowerRatio { get; set; }
    public double? SupplementalLoadAmount { get; set; }
    public BitmapImage Image { get; set; }
}
