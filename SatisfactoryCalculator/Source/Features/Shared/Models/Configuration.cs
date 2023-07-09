// ReSharper disable CheckNamespace

using SatisfactoryCalculator.Source.Features.Creature;
using SatisfactoryCalculator.Source.Features.Plant;
using FactoryConfiguration = SatisfactoryCalculator.Source.Features.FactoryPlanner.FactoryConfiguration;

#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models;

internal class Configuration : ObservableObject
{
    private ObservableCollection<IItem> _items = new();
    public ObservableCollection<IItem> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }
    
    private ObservableCollection<IEquipment> _equipments = new();
    public ObservableCollection<IEquipment> Equipments
    {
        get => _equipments;
        set => SetProperty(ref _equipments, value);
    }
    
    private ObservableCollection<IConsumable> _consumables = new();
    public ObservableCollection<IConsumable> Consumables
    {
        get => _consumables;
        set => SetProperty(ref _consumables, value);
    }
    
    private ObservableCollection<IWeapon> _weapons = new();
    public ObservableCollection<IWeapon> Weapons
    {
        get => _weapons;
        set => SetProperty(ref _weapons, value);
    }
    
    private ObservableCollection<IAmmunition> _ammunitions = new();
    public ObservableCollection<IAmmunition> Ammunitions
    {
        get => _ammunitions;
        set => SetProperty(ref _ammunitions, value);
    }
    
    private ObservableCollection<IResource> _resources = new();
    public ObservableCollection<IResource> Resources
    {
        get => _resources;
        set => SetProperty(ref _resources, value);
    }
    
    private ObservableCollection<IBuilding> _buildings = new();
    public ObservableCollection<IBuilding> Buildings
    {
        get => _buildings;
        set => SetProperty(ref _buildings, value);
    }
    
    private ObservableCollection<IGenerator> _generators = new();
    public ObservableCollection<IGenerator> Generators
    {
        get => _generators;
        set => SetProperty(ref _generators, value);
    }
    
    private ObservableCollection<IMiner> _miners = new();
    public ObservableCollection<IMiner> Miners
    {
        get => _miners;
        set => SetProperty(ref _miners, value);
    }
    
    private ObservableCollection<IRecipe> _recipes = new();
    public ObservableCollection<IRecipe> Recipes
    {
        get => _recipes;
        set => SetProperty(ref _recipes, value);
    }
    
    private ObservableCollection<ISchematic> _schematics = new();
    public ObservableCollection<ISchematic> Schematics
    {
        get => _schematics;
        set => SetProperty(ref _schematics, value);
    }
    
    private ObservableCollection<ICreature> _creatures = new();
    public ObservableCollection<ICreature> Creatures
    {
        get => _creatures;
        set => SetProperty(ref _creatures, value);
    }
    
    private ObservableCollection<IStatue> _statues = new();
    public ObservableCollection<IStatue> Statues
    {
        get => _statues;
        set => SetProperty(ref _statues, value);
    }
    
    private ObservableCollection<IPlant> _plants = new();
    public ObservableCollection<IPlant> Plants
    {
        get => _plants;
        set => SetProperty(ref _plants, value);
    }

    private DateTime _lastSyncDate = new();
    public DateTime LastSyncDate
    {
        get => _lastSyncDate;
        set => SetProperty(ref _lastSyncDate, value);
    }

    private ObservableCollection<FactoryConfiguration> _factoryConfigurations = new();
    public ObservableCollection<FactoryConfiguration> FactoryConfigurations
    {
        get => _factoryConfigurations;
        set => SetProperty(ref _factoryConfigurations, value);
    }
    
    internal IDictionary<string, EntityReference> ReferenceDictionary { get; set; }
}
