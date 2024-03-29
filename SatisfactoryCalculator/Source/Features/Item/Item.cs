namespace SatisfactoryCalculator.Source.Features.Item;

public class Item : IItem
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public Form? Form { get; init; }
    public decimal? EnergyValue { get; init; }
    public BitmapImage? Image { get; init; }
}
