// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

using System.Text.Json.Serialization;

#pragma warning disable CS8767
#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Item : IBase, IIcon
{
	public string ClassName { get; set; }
	public string DisplayName { get; set; }
	public string Description { get; set; }
	public string SmallIconPath { get; set; }
	public string BigIconPath { get; set; }
	public double EnergyValue { get; set; }
	public bool? IsRadioActive { get; set; }
	public double RadioActiveDecay { get; set; }
	public Form Form { get; set; }
	public StackSize StackSize { get; set; }
	public int? SinkPoints { get; set; }
}
