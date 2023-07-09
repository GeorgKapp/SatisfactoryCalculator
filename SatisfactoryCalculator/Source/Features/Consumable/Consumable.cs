namespace SatisfactoryCalculator.Source.Features.Consumable;

internal class Consumable : IConsumable
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public string Description { get; init; }
    public Form? Form { get; init; }
    public decimal? EnergyValue { get; init; }
    public decimal? HealthGain { get; init; }
}