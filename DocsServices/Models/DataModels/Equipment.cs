#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Equipment : IBase
{
	public string ClassName { get; set; }
	public EquipmentSlot EquipmentSlot { get; set; }
}
