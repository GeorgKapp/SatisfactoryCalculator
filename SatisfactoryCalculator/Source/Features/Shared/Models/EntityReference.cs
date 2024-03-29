﻿// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CreatureLoot = SatisfactoryCalculator.Source.Features.Creature.CreatureLoot;

#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models;

internal class EntityReference
{
    public IRecipe[] RecipeIngredient { get; init; }
    public IRecipe[] RecipeProduct { get; init;}
    public IRecipe[] RecipeBuildingIngredient { get;init; }
    public IRecipe[] RecipeBuilding { get; init; }
    public IRecipe[] RecipeFuels { get; init; }
    public ISchematic[] SchemaIngredient { get; init; }
    public ISchematic[] SchemaUnlock { get; init;}
    
    public GeneratorFuel[] FuelIngredient { get; init; }
    public GeneratorFuel[] FuelByProduct { get; init;}
    public GeneratorFuel[] FuelGenerator { get; init;}
    public CreatureLoot[] LootFromCreature { get; init; }
    public CreatureLoot[] DropsLoot { get; init; }
    public MinerResource[] MinerResource { get; init; }
}