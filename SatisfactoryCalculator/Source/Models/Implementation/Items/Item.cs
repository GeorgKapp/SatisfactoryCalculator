// ReSharper disable CheckNamespace
namespace SatisfactoryCalculator.Source.Models;

public class Item : IItem
{
    public Item(string className, string name, string description, Form? form, double energyValue, BitmapImage? image)
    {
        ClassName = className;
        Name = name;
        Description = description;
        Form = form;
        EnergyValue = energyValue;
        Image = image;
    }

#pragma warning disable CS8618
    public Item(string className, string name, Form? form, double energyValue)
#pragma warning restore CS8618
    {
        ClassName = className;
        Name = name;
        Form = form;
        EnergyValue = energyValue;
    }

    public string ClassName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Form? Form { get; set; }
    public double EnergyValue { get; set; }
    public BitmapImage? Image { get; set; }
}
