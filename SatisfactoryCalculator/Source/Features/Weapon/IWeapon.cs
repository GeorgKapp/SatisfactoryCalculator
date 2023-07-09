namespace SatisfactoryCalculator.Source.Features.Weapon;

internal interface IWeapon : IItem
{
    decimal? DamageMultiplier { get; init; }
    decimal? ReloadTime { get; init; }
    decimal? AutoReloadDelay { get; init; }
    EquipmentSlot EquipmentSlot { get; init; }
    public IAmmunition[] Ammunitions { get; set; }
}