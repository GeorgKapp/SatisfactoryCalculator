namespace SatisfactoryCalculator.Source.Features.Shared.Models;

internal interface IEntity
{
    string ClassName { get; }
    string Name { get; }
    public BitmapImage? Image { get; }
}