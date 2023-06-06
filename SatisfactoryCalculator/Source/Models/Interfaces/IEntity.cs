namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IEntity
{
    string ClassName { get; }
    string Name { get; }
    public BitmapImage? Image { get; }
}