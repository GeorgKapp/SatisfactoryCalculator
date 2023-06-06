namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IEquipment : IItem
{
    EquipmentSlot EquipmentSlot { get; init; }
}