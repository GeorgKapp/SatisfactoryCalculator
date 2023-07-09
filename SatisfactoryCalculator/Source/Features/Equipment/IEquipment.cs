namespace SatisfactoryCalculator.Source.Features.Equipment;

internal interface IEquipment : IItem
{
    EquipmentSlot EquipmentSlot { get; init; }
}