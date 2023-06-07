namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IAmmunition : IItem
{
    public double MaxAmmoEffectiveRange { get; init; }
    public double? ReloadTimeMultiplier { get; init; }
    public double FireRate { get; init; }
    public double WeaponDamageMultiplier { get; init; }
    public IWeapon UsedInWeapon { get; set; }
}