namespace SatisfactoryCalculator.Source.Models;

public class ConfigurationModel : ObservableObject
{
    private ObservableCollection<RecipeModel> _recipes = new();
    internal ObservableCollection<RecipeModel> Recipes
    {
        get => _recipes;
        set => SetProperty(ref _recipes, value);
    }

    private ObservableCollection<RecipeModel> _availableRecipes = new();
    internal ObservableCollection<RecipeModel> AvailableRecipes
    {
        get => _availableRecipes;
        set => SetProperty(ref _availableRecipes, value);
    }

    private ObservableCollection<BuildingModel> _buildings = new();
    internal ObservableCollection<BuildingModel> Buildings
    {
        get => _buildings;
        set => SetProperty(ref _buildings, value);
    }

    private ObservableCollection<BuildingModel> _availableBuildings = new();
    internal ObservableCollection<BuildingModel> AvailableBuildings
    {
        get => _availableBuildings;
        set => SetProperty(ref _availableBuildings, value);
    }

    private ObservableCollection<ItemModel> _items = new();
    internal ObservableCollection<ItemModel> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }

    private ObservableCollection<ItemModel> _availableItems = new();
    internal ObservableCollection<ItemModel> AvailableItems
    {
        get => _availableItems;
        set => SetProperty(ref _availableItems, value);
    }

    private ObservableCollection<GeneratorModel> _generators = new();
    internal ObservableCollection<GeneratorModel> Generators
    {
        get => _generators;
        set => SetProperty(ref _generators, value);
    }

    private ObservableCollection<GeneratorModel> _availableGenerators = new();
    internal ObservableCollection<GeneratorModel> AvailableGenerators
    {
        get => _availableGenerators;
        set => SetProperty(ref _availableGenerators, value);
    }

    private DateTime? _lastSyncDate = new();
    internal DateTime? LastSyncDate
    {
        get => _lastSyncDate;
        set => SetProperty(ref _lastSyncDate, value);
    }

    internal Dictionary<string, EntityReference> ReferenceDictionary { get; set; } = new();
}
