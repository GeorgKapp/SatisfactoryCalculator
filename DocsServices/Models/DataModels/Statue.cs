#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Statue : IBase , IIcon
{
	public string ClassName { get; set; }
	public string DisplayName { get; set; }
	public string Description { get; set; }
	public string? SmallIconPath { get; set; }
	public string? BigIconPath { get; set; }
}
