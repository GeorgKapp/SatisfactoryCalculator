namespace SatisfactoryCalculator.Source.Models;

public class ConfigurationModel : ObservableObject
{
    private ObservableCollection<RecipeModel> _recipes = new();
    public ObservableCollection<RecipeModel> Recipes
    {
        get => _recipes;
        set => SetProperty(ref _recipes, value);
    }

    private ObservableCollection<RecipeModel> _availableRecipes = new();
    public ObservableCollection<RecipeModel> AvailableRecipes
    {
        get => _availableRecipes;
        set => SetProperty(ref _availableRecipes, value);
    }

    private ObservableCollection<BuildingModel> _buildings = new();
    public ObservableCollection<BuildingModel> Buildings
    {
        get => _buildings;
        set => SetProperty(ref _buildings, value);
    }

    private ObservableCollection<BuildingModel> _availableBuildings = new();
    public ObservableCollection<BuildingModel> AvailableBuildings
    {
        get => _availableBuildings;
        set => SetProperty(ref _availableBuildings, value);
    }

    private ObservableCollection<ItemModel> _items = new();
    public ObservableCollection<ItemModel> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }

    private ObservableCollection<ItemModel> _availableItems = new();
    public ObservableCollection<ItemModel> AvailableItems
    {
        get => _availableItems;
        set => SetProperty(ref _availableItems, value);
    }

    private DateTime? _lastSyncDate = new();
    public DateTime? LastSyncDate
    {
        get => _lastSyncDate;
        set => SetProperty(ref _lastSyncDate, value);
    }

    public Dictionary<string, BuildingModel> BuildingDictionary { get; set; } = new();
    public Dictionary<string, ItemModel> ItemDictionary { get; set; } = new();
    public Dictionary<string, ItemRecipeModel> ItemRecipesDictionary { get; set; } = new();
    public Dictionary<string, ItemRecipeModel> BuildingRecipesDictionary { get; set; } = new();
}
