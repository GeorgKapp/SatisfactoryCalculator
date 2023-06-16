namespace Data.Models.Implementation;

public class Weapon : IClassNamePrimaryKey
{
    public string ClassName { get; set; }
    public Item Item { get; set; }
    
    public decimal? DamageMultiplier { get; set; }
    public decimal? ReloadTime { get; set; }
    public decimal? AutoReloadDelay { get; set; }
    public EquipmentSlot EquipmentSlot { get; set; }
    
    public ICollection<Ammunition> Ammunitions { get; set; } = new List<Ammunition>();
}
