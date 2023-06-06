namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IItem : IEntity
{
    string Description { get; }
    Form? Form { get; }
    double EnergyValue { get; }
}