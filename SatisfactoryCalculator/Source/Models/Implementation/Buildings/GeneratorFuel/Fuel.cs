// ReSharper disable CheckNamespace
namespace SatisfactoryCalculator.Source.Models;

internal class Fuel
{
    public Fuel(IGenerator generator, FuelItem ingredient)
    {
        Generator = generator;
        Ingredient = ingredient;
    }

    public Fuel(IGenerator generator, FuelItem ingredient, FuelItem? supplementalIngredient)
    {
        Generator = generator;
        Ingredient = ingredient;
        SupplementalIngredient = supplementalIngredient;
    }

    public Fuel(IGenerator generator, FuelItem ingredient, FuelItem? supplementalIngredient, FuelItem? byProduct, double? byProductAmount)
    {
        Ingredient = ingredient;
        SupplementalIngredient = supplementalIngredient;
        ByProduct = byProduct;
        ByProductAmount = byProductAmount;
        Generator = generator;
    }

    public IGenerator Generator { get; set; }
    public FuelItem Ingredient { get; set; }
    public FuelItem? SupplementalIngredient { get; set; }
    public FuelItem? ByProduct { get; set; }
    public double? ByProductAmount { get; set; }
}