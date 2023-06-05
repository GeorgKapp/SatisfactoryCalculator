﻿using Fuel = SatisfactoryCalculator.Source.Models.Fuel;
using Generator = SatisfactoryCalculator.Source.Models.Generator;
using Recipe = SatisfactoryCalculator.Source.Models.Recipe;

namespace SatisfactoryCalculator.Source.ViewModels;

internal class GeneratorViewModel : ObservableObject
{
    private IGenerator? _selectedGenerator;
    public IGenerator? SelectedGenerator
    {
        get => _selectedGenerator;
        set
        {
            SetProperty(ref _selectedGenerator, value);

            if (value is null)
            {
                SelectedGeneratorFuels = new();
                SelectedGeneratorRecipes = new();
            }
            else
            {
                SelectedGeneratorFuels = new(_applicationState.Configuration.ReferenceDictionary[value.ClassName].FuelGenerator);
                SelectedGeneratorRecipes = new(_applicationState.Configuration.ReferenceDictionary[value.ClassName].RecipeProduct);
                SelectedGeneratorClockSpeed = "100";
            }
        }
    }

    private ObservableCollection<Fuel> _selectedGeneratorFuels = new();
    public ObservableCollection<Fuel> SelectedGeneratorFuels
    {
        get => _selectedGeneratorFuels;
        set => SetProperty(ref _selectedGeneratorFuels, value);
    }

    private ObservableCollection<IRecipe> _selectedGeneratorRecipes = new();
    public ObservableCollection<IRecipe> SelectedGeneratorRecipes
    {
        get => _selectedGeneratorRecipes;
        set => SetProperty(ref _selectedGeneratorRecipes, value);
    }

    private string _selectedGeneratorClockSpeed = string.Empty;
    public string SelectedGeneratorClockSpeed
    {
        get => _selectedGeneratorClockSpeed;
        set
        {
            SetProperty(ref _selectedGeneratorClockSpeed, value);
            
            var selectedGeneratorClockSpeed = string.IsNullOrEmpty(value) 
                ? 0 
                : double.Parse(value);
            
            foreach(var fuelItem in SelectedGeneratorFuels)
            {
                var fuelConsumptionResult = _calculationService.CalculateRoundedFuelConsumption(fuelItem, selectedGeneratorClockSpeed);
                
                fuelItem.Ingredient.AmountPerMinute = fuelConsumptionResult.AmountPerMinute;
                
                if (fuelItem.SupplementalIngredient is not null) 
                    fuelItem.SupplementalIngredient.AmountPerMinute = fuelConsumptionResult.SupplementalAmountPerMinute;
                
                if (fuelItem.ByProduct is not null) 
                    fuelItem.ByProduct.AmountPerMinute = fuelConsumptionResult.ByProductAmountPerMinute;
            }
        }
    }
    
    public GeneratorViewModel(ApplicationState applicationState, CalculationService calculationService)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
    }

    private readonly ApplicationState _applicationState;
    private readonly CalculationService _calculationService;
}