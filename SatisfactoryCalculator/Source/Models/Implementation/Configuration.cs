namespace SatisfactoryCalculator.Source.Models;

public class Configuration : ObservableObject
{
    private ObservableCollection<IItem> _items = new();
    internal ObservableCollection<IItem> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }
    
    private ObservableCollection<IEquipment> _equipments = new();
    internal ObservableCollection<IEquipment> Equipments
    {
        get => _equipments;
        set => SetProperty(ref _equipments, value);
    }
    
    private ObservableCollection<IBuilding> _buildings = new();
    internal ObservableCollection<IBuilding> Buildings
    {
        get => _buildings;
        set => SetProperty(ref _buildings, value);
    }
    
    private ObservableCollection<IGenerator> _generators = new();
    internal ObservableCollection<IGenerator> Generators
    {
        get => _generators;
        set => SetProperty(ref _generators, value);
    }
    
    private ObservableCollection<IRecipe> _recipes = new();
    internal ObservableCollection<IRecipe> Recipes
    {
        get => _recipes;
        set => SetProperty(ref _recipes, value);
    }

    private DateTime? _lastSyncDate = new();
    internal DateTime? LastSyncDate
    {
        get => _lastSyncDate;
        set => SetProperty(ref _lastSyncDate, value);
    }

    internal Dictionary<string, EntityReference> ReferenceDictionary { get; set; } = new();
}
