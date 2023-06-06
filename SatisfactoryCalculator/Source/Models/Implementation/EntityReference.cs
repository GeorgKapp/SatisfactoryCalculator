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

    public ISchema[] SchemaIngredient { get; init; }
    public ISchema[] SchemaUnlock { get; init;}
    public ISchema[] SchemaBuilding { get; init; }
}