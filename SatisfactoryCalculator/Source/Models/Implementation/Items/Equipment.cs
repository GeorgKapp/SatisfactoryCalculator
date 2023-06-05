// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.Models;

internal class Equipment : IEquipment
{
    public Equipment(string className, string name, BitmapImage? image, string description, Form? form, double energyValue, EquipmentSlot equipmentSlot)
    {
        ClassName = className;
        Name = name;
        Image = image;
        Description = description;
        Form = form;
        EnergyValue = energyValue;
        EquipmentSlot = equipmentSlot;
    }

    public string ClassName { get; set; }
    public string Name { get; set; }
    public BitmapImage? Image { get; set; }
    public string Description { get; set; }
    public Form? Form { get; set; }
    public double EnergyValue { get; set; }
    public EquipmentSlot EquipmentSlot { get; set; }
}