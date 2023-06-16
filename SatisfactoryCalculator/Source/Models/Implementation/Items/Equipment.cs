// ReSharper disable once CheckNamespace
#pragma warning disable CS8618
// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.Models;

internal class Equipment : IEquipment
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public string Description { get; init; }
    public Form? Form { get; init; }
    public decimal? EnergyValue { get; init; }
    public EquipmentSlot EquipmentSlot { get; init; }
}