namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IConsumable : IItem
{
    public double? HealthGain { get; set; }
}