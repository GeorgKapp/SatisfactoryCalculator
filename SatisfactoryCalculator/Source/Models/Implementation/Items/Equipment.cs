namespace SatisfactoryCalculator.Source.Models;

internal class Equipment : IEquipment
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public BitmapImage? Image { get; set; }
    public string Description { get; set; }
    public Form? Form { get; set; }
    public double EnergyValue { get; set; }
    public EquipmentSlot EquipmentSlot { get; set; }
}