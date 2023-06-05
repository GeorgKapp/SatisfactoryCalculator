namespace SatisfactoryCalculator.Source.Models;

internal class Consumable : IConsumable
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public BitmapImage? Image { get; set; }
    public string Description { get; set; }
    public Form? Form { get; set; }
    public double EnergyValue { get; set; }
    public double? HealthGain { get; set; }
}