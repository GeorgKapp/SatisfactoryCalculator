namespace SatisfactoryCalculator.Source.Models;

public class Configuration : ObservableObject
{
    private ObservableCollection<Recipe> _recipes = new();
    internal ObservableCollection<Recipe> Recipes
    {
        get => _recipes;
        set => SetProperty(ref _recipes, value);
    }
    
    private ObservableCollection<Building> _buildings = new();
    internal ObservableCollection<Building> Buildings
    {
        get => _buildings;
        set => SetProperty(ref _buildings, value);
    }

    private ObservableCollection<Item> _items = new();
    internal ObservableCollection<Item> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }

    private ObservableCollection<Generator> _generators = new();
    internal ObservableCollection<Generator> Generators
    {
        get => _generators;
        set => SetProperty(ref _generators, value);
    }

    private DateTime? _lastSyncDate = new();
    internal DateTime? LastSyncDate
    {
        get => _lastSyncDate;
        set => SetProperty(ref _lastSyncDate, value);
    }

    internal Dictionary<string, EntityReference> ReferenceDictionary { get; set; } = new();
}
