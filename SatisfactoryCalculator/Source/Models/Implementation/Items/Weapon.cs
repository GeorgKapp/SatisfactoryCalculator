namespace SatisfactoryCalculator.Source.Models;

internal class Weapon : IWeapon
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public string Description { get; init; }
    public Form? Form { get; init; }
    public double EnergyValue { get; init; }
    public double? DamageMultiplier { get; init; }
    public double? ReloadTime { get; init; }
    public double? AutoReloadDelay { get; init; }
    public EquipmentSlot EquipmentSlot { get; init; }
}