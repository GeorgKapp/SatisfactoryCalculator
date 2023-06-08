namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Creature : IBase, IIcon
{
    public string ClassName { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public string SmallIconPath { get; set; }
    public string BigIconPath { get; set; }
    public string? VariantGroup { get; set; }
    public int HitPoints { get; set; }
    public string[]? Damage { get; set; }
    public CreatureLoot[] Loot { get; set; } = Array.Empty<CreatureLoot>();
    public CreatureBehaviour Behaviour { get; set; }
}