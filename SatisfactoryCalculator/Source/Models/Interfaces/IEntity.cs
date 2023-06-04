namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IEntity
{
    string ClassName { get; set; }
    string Name { get; set; }
    public BitmapImage? Image { get; set; }
}