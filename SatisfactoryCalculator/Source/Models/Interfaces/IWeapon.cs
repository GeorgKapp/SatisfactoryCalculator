namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IWeapon : IItem
{
    double? DamageMultiplier { get; init; }
    double? ReloadTime { get; init; }
    double? AutoReloadDelay { get; init; }
    EquipmentSlot EquipmentSlot { get; init; }
}