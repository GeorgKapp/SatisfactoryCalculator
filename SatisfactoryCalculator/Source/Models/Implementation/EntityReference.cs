// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models.Refernces;

internal class EntityReference
{
    public IRecipe[] RecipeIngredient { get; init; }
    public IRecipe[] RecipeProduct { get; init;}
    public IRecipe[] RecipeBuildingIngredient { get;init; }
    public IRecipe[] RecipeBuilding { get; init; }
    public IRecipe[] RecipeFuels { get; init; }
    public Fuel[] FuelIngredient { get; init; }
    public Fuel[] FuelByProduct { get; init;}
    public Fuel[] FuelGenerator { get; init;}
    public IWeapon? IsMunitionForWeapon { get; init;}
    public IAmmunition[] UsesAmmunition { get; init;}
    public ISchematic[] SchemaIngredient { get; init; }
    public ISchematic[] SchemaUnlock { get; init;}
}