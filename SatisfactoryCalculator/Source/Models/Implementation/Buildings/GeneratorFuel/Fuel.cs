﻿// ReSharper disable CheckNamespace
#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models;

internal class Fuel
{
    public IGenerator Generator { get; init; }
    public FuelItem Ingredient { get; init; }
    public FuelItem? SupplementalIngredient { get; init; }
    public FuelItem? ByProduct { get; init; }
    public double? ByProductAmount { get; init; }
}