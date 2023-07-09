namespace SatisfactoryCalculator.Source.Features.Item;

internal interface IItem : IEntity
{
    string Description { get; }
    Form? Form { get; }
    decimal? EnergyValue { get; }
}