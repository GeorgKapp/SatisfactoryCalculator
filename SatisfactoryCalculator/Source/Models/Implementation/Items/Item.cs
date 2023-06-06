// ReSharper disable CheckNamespace
#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models;

public class Item : IItem
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public Form? Form { get; init; }
    public double EnergyValue { get; init; }
    public BitmapImage? Image { get; init; }
}
