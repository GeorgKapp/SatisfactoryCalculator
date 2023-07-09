namespace SatisfactoryCalculator.Source.Features.Ammunition;

internal interface IAmmunition : IItem
{
    public decimal MaxAmmoEffectiveRange { get; init; }
    public decimal? ReloadTimeMultiplier { get; init; }
    public decimal FireRate { get; init; }
    public decimal WeaponDamageMultiplier { get; init; }
    public IWeapon UsedInWeapon { get; set; }
}