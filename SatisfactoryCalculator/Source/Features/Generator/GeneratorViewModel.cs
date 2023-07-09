namespace SatisfactoryCalculator.Source.Features.Generator;

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

    private string _selectedGeneratorClockSpeed = "0";
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
                _modelCalculationService.CalculateAndApplyRoundedFuelConsumption(
                    selectedGeneratorClockSpeed,
                    generatorFuel);
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