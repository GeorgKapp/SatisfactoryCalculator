#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models;

internal class Weapon : IWeapon
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public string Description { get; init; }
    public Form? Form { get; init; }
    public decimal? EnergyValue { get; init; }
    public decimal? DamageMultiplier { get; init; }
    public decimal? ReloadTime { get; init; }
    public decimal? AutoReloadDelay { get; init; }
    public EquipmentSlot EquipmentSlot { get; init; }
    public IAmmunition[] Ammunitions { get; set; }
}