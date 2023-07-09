namespace SatisfactoryCalculator.Source.Features.Equipment;

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