namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IItem : IEntity
{
    string Description { get; set; }
    Form? Form { get; set; }
    double EnergyValue { get; set; }
}