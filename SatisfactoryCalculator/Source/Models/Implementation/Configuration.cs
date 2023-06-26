// ReSharper disable CheckNamespace
#pragma warning disable CS8618
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
    
    private ObservableCollection<IConsumable> _consumables = new();
    internal ObservableCollection<IConsumable> Consumables
    {
        get => _consumables;
        set => SetProperty(ref _consumables, value);
    }
    
    private ObservableCollection<IWeapon> _weapons = new();
    internal ObservableCollection<IWeapon> Weapons
    {
        get => _weapons;
        set => SetProperty(ref _weapons, value);
    }
    
    private ObservableCollection<IAmmunition> _ammunitions = new();
    internal ObservableCollection<IAmmunition> Ammunitions
    {
        get => _ammunitions;
        set => SetProperty(ref _ammunitions, value);
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
    
    private ObservableCollection<ISchematic> _schematics = new();
    internal ObservableCollection<ISchematic> Schematics
    {
        get => _schematics;
        set => SetProperty(ref _schematics, value);
    }
    
    private ObservableCollection<ICreature> _creatures = new();
    internal ObservableCollection<ICreature> Creatures
    {
        get => _creatures;
        set => SetProperty(ref _creatures, value);
    }
    
    private ObservableCollection<IPlant> _plants = new();
    internal ObservableCollection<IPlant> Plants
    {
        get => _plants;
        set => SetProperty(ref _plants, value);
    }
    
    private ObservableCollection<IResource> _resources = new();
    internal ObservableCollection<IResource> Resources
    {
        get => _resources;
        set => SetProperty(ref _resources, value);
    }

    private DateTime? _lastSyncDate = new();
    internal DateTime? LastSyncDate
    {
        get => _lastSyncDate;
        set => SetProperty(ref _lastSyncDate, value);
    }

    internal IDictionary<string, EntityReference> ReferenceDictionary { get; set; }
}
