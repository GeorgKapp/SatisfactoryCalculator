// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Weapon : IBase
{
#pragma warning disable CS8618
	public string ClassName { get; set; }
#pragma warning restore CS8618
	public double? DamageMultiplier { get; set; }
	public double? ReloadTime { get; set; }
	public double? AutoReloadDelay { get; set; }
	public EquipmentSlot EquipmentSlot { get; set; }
}
