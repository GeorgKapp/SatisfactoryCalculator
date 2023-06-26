﻿namespace SatisfactoryCalculator.Source.ViewModels;

internal class GeneratorViewModel : ObservableObject
{
    private IGenerator _selectedGenerator;
    public IGenerator SelectedGenerator
    {
        get => _selectedGenerator;
        set
        {
            SetProperty(ref _selectedGenerator, value);
            
            SelectedGeneratorFuels = new(_applicationState.Configuration.ReferenceDictionary[value.ClassName].FuelGenerator);
            SelectedGeneratorRecipes = new(_applicationState.Configuration.ReferenceDictionary[value.ClassName].RecipeProduct);
            SelectedGeneratorClockSpeed = "100";
        }
    }

    private ObservableCollection<GeneratorFuel> _selectedGeneratorFuels = new();
    public ObservableCollection<GeneratorFuel> SelectedGeneratorFuels
    {
        get => _selectedGeneratorFuels;
        private set => SetProperty(ref _selectedGeneratorFuels, value);
    }

    private ObservableCollection<IRecipe> _selectedGeneratorRecipes = new();
    public ObservableCollection<IRecipe> SelectedGeneratorRecipes
    {
        get => _selectedGeneratorRecipes;
        private set => SetProperty(ref _selectedGeneratorRecipes, value);
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
            
            foreach(var generatorFuel in SelectedGeneratorFuels)
            {
                var fuelConsumptionResult = _modelCalculationService.CalculateRoundedFuelConsumption(
                    selectedGeneratorClockSpeed,
                    generatorFuel);
                
                generatorFuel.Ingredient.AmountPerMinute = fuelConsumptionResult.AmountPerMinute;
                
                if (generatorFuel.SupplementalIngredient is not null) 
                    generatorFuel.SupplementalIngredient.AmountPerMinute = fuelConsumptionResult.SupplementalAmountPerMinute;
                
                if (generatorFuel.ByProduct is not null) 
                    generatorFuel.ByProduct.AmountPerMinute = fuelConsumptionResult.ByProductAmountPerMinute;
            }
        }
    }
    
#pragma warning disable CS8618
    public GeneratorViewModel(ApplicationState applicationState, ModelCalculationService modelCalculationService)
#pragma warning restore CS8618
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        _modelCalculationService = modelCalculationService ?? throw new ArgumentNullException(nameof(modelCalculationService));
    }

    private readonly ApplicationState _applicationState;
    private readonly ModelCalculationService _modelCalculationService;
}