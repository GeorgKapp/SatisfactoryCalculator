// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class DataModelMappingResult
{
    public DataModelMappingResult(
        IItem[] items, 
        IEquipment[] equipments, 
        IConsumable[] consumables, 
        IWeapon[] weapons,
        IAmmunition[] ammunitions,
        IBuilding[] buildings,
        IGenerator[] generators, 
        IRecipe[] recipes, 
        IDictionary<string, EntityReference> referenceDictionary, 
        DateTime? lastSyncDate)
    {
        Items = items;
        Equipments = equipments;
        Consumables = consumables;
        Weapons = weapons;
        Ammunitions = ammunitions;
        Buildings = buildings;
        Generators = generators;
        Recipes = recipes;
        ReferenceDictionary = referenceDictionary;
        LastSyncDate = lastSyncDate;
    }

    public IDictionary<string, EntityReference> ReferenceDictionary { get; }
    public IItem[] Items { get; }
    public IEquipment[] Equipments { get; }
    public IConsumable[] Consumables { get; }
    public IWeapon[] Weapons { get; }
    public IAmmunition[] Ammunitions { get; }
    public IBuilding[] Buildings { get; }
    public IGenerator[] Generators { get; }
    public IRecipe[] Recipes { get; }
    public DateTime? LastSyncDate { get; }
}
