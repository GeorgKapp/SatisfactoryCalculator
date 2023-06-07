// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Ammunition : IBase
{
	public string ClassName { get; set; }
	public double MaxAmmoEffectiveRange { get; set; }
	public double? ReloadTimeMultiplier { get; set; }
	public double FireRate { get; set; }
	public double WeaponDamageMultiplier { get; set; }
	public string UsedInWeapon { get; set; }
}
