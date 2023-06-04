namespace SatisfactoryCalculator.Source.Models;

public class Item : IItem
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Form? Form { get; set; }
    public double EnergyValue { get; set; }
    public BitmapImage Image { get; set; }
}
