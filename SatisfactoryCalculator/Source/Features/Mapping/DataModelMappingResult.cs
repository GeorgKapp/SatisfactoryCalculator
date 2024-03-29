﻿// ReSharper disable once CheckNamespace

using SatisfactoryCalculator.Source.Features.Creature;
using FactoryConfiguration = SatisfactoryCalculator.Source.Features.FactoryPlanner.FactoryConfiguration;

namespace SatisfactoryCalculator.Source.Features.Mapping;

internal class DataModelMappingResult
{
    public DataModelMappingResult(
        IItem[] items, 
        IEquipment[] equipments, 
        IConsumable[] consumables, 
        IWeapon[] weapons,
        IAmmunition[] ammunitions,
        IResource[] resources,
        IBuilding[] buildings,
        IGenerator[] generators,
        IMiner[] miners,
        IRecipe[] recipes, 
        ICreature[] creatures,
        IStatue[] statues,
        FactoryConfiguration[] factoryConfigurations,
        IDictionary<string, EntityReference> referenceDictionary, 
        DateTime lastSyncDate)
    {
        Items = items;
        Equipments = equipments;
        Consumables = consumables;
        Weapons = weapons;
        Ammunitions = ammunitions;
        Resources = resources;
        Buildings = buildings;
        Generators = generators;
        Miners = miners;
        Recipes = recipes;
        Creatures = creatures;
        Statues = statues;
        FactoryConfigurations = factoryConfigurations;
        ReferenceDictionary = referenceDictionary;
        LastSyncDate = lastSyncDate;
    }
    
    public IItem[] Items { get; }
    public IEquipment[] Equipments { get; }
    public IConsumable[] Consumables { get; }
    public IWeapon[] Weapons { get; }
    public IAmmunition[] Ammunitions { get; }
    public IResource[] Resources { get; }
    public IBuilding[] Buildings { get; }
    public IGenerator[] Generators { get; }
    public IMiner[] Miners { get; }
    public IRecipe[] Recipes { get; }
    public ICreature[] Creatures { get; }
    public IStatue[] Statues { get; }
    public FactoryConfiguration[] FactoryConfigurations { get; }
    public IDictionary<string, EntityReference> ReferenceDictionary { get; }
    public DateTime LastSyncDate { get; }
}
