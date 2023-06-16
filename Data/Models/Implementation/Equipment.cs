namespace Data.Models.Implementation;

public class Equipment : IClassNamePrimaryKey
{
    public string ClassName { get; set; }
    public Item Item { get; set; }

    public EquipmentSlot EquipmentSlot { get; set; }
}
