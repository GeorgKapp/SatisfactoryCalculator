namespace Data.Models.Implementation;

public class Ammunition : IClassNamePrimaryKey
{
    public string ClassName { get; set; }
    public Item Item { get; set; }
    
    public decimal MaxAmmoEffectiveRange { get; set; }
    public decimal ReloadTimeMultiplier { get; set; }
    public decimal FireRate { get; set; }
    public decimal WeaponDamageMultiplier { get; set; }
    
    public string WeaponClassName { get; set; }
    public Weapon Weapon { get; set; }
}
