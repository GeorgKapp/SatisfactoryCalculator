namespace SatisfactoryCalculator.Source.Models;

internal class Resource : IResource
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public BitmapImage? Image { get; set; }
    public string Description { get; set; }
    public Form? Form { get; set; }
    public decimal? EnergyValue { get; set; }
}
