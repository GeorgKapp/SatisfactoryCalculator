namespace SatisfactoryCalculator.Source.Models;

internal class Plant : IPlant
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image => null;
}
