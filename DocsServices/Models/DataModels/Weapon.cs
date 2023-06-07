// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Weapon : IBase
{

	public string ClassName { get; set; }
	public double? DamageMultiplier { get; set; }
	public double? ReloadTime { get; set; }
	public double? AutoReloadDelay { get; set; }
	public EquipmentSlot EquipmentSlot { get; set; }
	public string[] UsesAmmunition { get; set; }
}
