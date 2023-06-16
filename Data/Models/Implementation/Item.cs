namespace Data.Models.Implementation;

public class Item : IClassNamePrimaryKey, INameEntity, IDescriptionEntity, IImage
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? SmallImagePath { get; set; }
    public string? BigImagePath { get; set; }
    public Form? Form { get; set; }
    public StackSize StackSize { get; set; }
    public decimal? EnergyValue { get; set; }
    public bool IsRadioActive { get; set; }
    public decimal? RadioActiveDecay { get; set; }
    public int? SinkPoints { get; set; }
    public Ammunition? Ammunition { get; set; }
    public Weapon? Weapon { get; set; }
    public Consumable? Consumable { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Resource? Resource { get; set; }
    public Equipment? Equipment { get; set; }
}
