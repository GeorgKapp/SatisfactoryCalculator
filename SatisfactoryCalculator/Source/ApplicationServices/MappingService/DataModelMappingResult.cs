namespace SatisfactoryCalculator.Source.ApplicationServices.MappingService;

internal class DataModelMappingResult
{
    public Dictionary<string, BuildingModel> BuildingDictionary { get; set; } = new();
    public Dictionary<string, ItemModel> ItemDictionary { get; set; } = new();
    public Dictionary<string, ItemRecipeModel> ItemRecipesDictionary { get; set; } = new();
    public Dictionary<string, ItemRecipeModel> BuildingRecipesDictionary { get; set; } = new();

    public ItemModel[] Items { get; set; } = Array.Empty<ItemModel>();
    public BuildingModel[] Buildings { get; set; } = Array.Empty<BuildingModel>();
    public RecipeModel[] Recipes { get; set; } = Array.Empty<RecipeModel>();

    public DateTime? LastSyncDate { get; set; }
}
