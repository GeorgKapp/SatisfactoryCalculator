// ReSharper disable CheckNamespace
namespace SatisfactoryCalculator.Source.Models;

internal class Consumable : IConsumable
{
    public string ClassName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public BitmapImage? Image { get; set; }
    public string Description { get; set; } = null!;
    public Form? Form { get; set; }
    public double EnergyValue { get; set; }
    public double? HealthGain { get; set; }
}