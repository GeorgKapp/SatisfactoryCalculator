namespace SatisfactoryCalculator.Source.Models;

internal class Statue : IStatue
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public string Description { get; init; }
}
