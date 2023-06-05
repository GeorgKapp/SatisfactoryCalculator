using Building = SatisfactoryCalculator.Source.Models.Building;
using Fuel = SatisfactoryCalculator.Source.Models.Fuel;
using Generator = SatisfactoryCalculator.Source.Models.Generator;
using Item = SatisfactoryCalculator.Source.Models.Item;
using Recipe = SatisfactoryCalculator.Source.Models.Recipe;

// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class DataModelMappingResult
{
    public Dictionary<string, EntityReference> ReferenceDictionary { get; set; } = new();

    public IItem[] Items { get; set; } = Array.Empty<IItem>();
    public IEquipment[] Equipments { get; set; } = Array.Empty<IEquipment>();
    public IConsumable[] Consumables { get; set; } = Array.Empty<IConsumable>();
    public IBuilding[] Buildings { get; set; } = Array.Empty<IBuilding>();
    public IGenerator[] Generators { get; set; } = Array.Empty<IGenerator>();
    public IRecipe[] Recipes { get; set; } = Array.Empty<IRecipe>();
    public DateTime? LastSyncDate { get; set; }
}
