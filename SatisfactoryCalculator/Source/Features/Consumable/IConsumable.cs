namespace SatisfactoryCalculator.Source.Features.Consumable;

internal interface IConsumable : IItem
{
    public decimal? HealthGain { get; init; }
}