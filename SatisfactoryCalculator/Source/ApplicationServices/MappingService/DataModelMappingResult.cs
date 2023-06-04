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

    public Item[] Items { get; set; } = Array.Empty<Item>();
    public Building[] Buildings { get; set; } = Array.Empty<Building>();
    public Generator[] Generators { get; set; } = Array.Empty<Generator>();
    public Fuel[] Fuels { get; set; } = Array.Empty<Fuel>();
    public Recipe[] Recipes { get; set; } = Array.Empty<Recipe>();
    public DateTime? LastSyncDate { get; set; }
}
