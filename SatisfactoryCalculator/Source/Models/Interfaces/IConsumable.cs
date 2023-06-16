namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IConsumable : IItem
{
    public decimal? HealthGain { get; init; }
}