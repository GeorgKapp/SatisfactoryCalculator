using SatisfactoryCalculator.Source.Models.Refernces;

namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class DataModelMappingResult
{
    public Dictionary<string, EntityReference> ReferenceDictionary { get; set; } = new();

    public ItemModel[] Items { get; set; } = Array.Empty<ItemModel>();
    public BuildingModel[] Buildings { get; set; } = Array.Empty<BuildingModel>();
    public GeneratorModel[] Generators { get; set; } = Array.Empty<GeneratorModel>();
    public FuelModel[] Fuels { get; set; } = Array.Empty<FuelModel>();
    public RecipeModel[] Recipes { get; set; } = Array.Empty<RecipeModel>();

    public DateTime? LastSyncDate { get; set; }
}
