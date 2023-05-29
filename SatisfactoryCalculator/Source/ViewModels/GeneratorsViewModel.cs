namespace SatisfactoryCalculator.Source.ViewModels;

internal class GeneratorsViewModel : ObservableObject
{

    private GeneratorModel? _selectedGenerator;
    public GeneratorModel? SelectedGenerator
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
                SelectedGeneratorFuels = new ObservableCollection<FuelModel>(_applicationState.Configuration.ReferenceDictionary[value.ClassName].FuelGenerator);
                SelectedGeneratorRecipes = new ObservableCollection<RecipeModel>(_applicationState.Configuration.ReferenceDictionary[value.ClassName].RecipeProduct);
                SelectedGeneratorClockSpeed = 100;
            }
        }
    }

    private ObservableCollection<FuelModel> _selectedGeneratorFuels = new();
    public ObservableCollection<FuelModel> SelectedGeneratorFuels
    {
        get => _selectedGeneratorFuels;
        set => SetProperty(ref _selectedGeneratorFuels, value);
    }

    private ObservableCollection<RecipeModel> _selectedGeneratorRecipes = new();
    public ObservableCollection<RecipeModel> SelectedGeneratorRecipes
    {
        get => _selectedGeneratorRecipes;
        set => SetProperty(ref _selectedGeneratorRecipes, value);
    }

    private int _selectedGeneratorClockSpeed;
    public int SelectedGeneratorClockSpeed
    {
        get => _selectedGeneratorClockSpeed;
        set
        {
            SetProperty(ref _selectedGeneratorClockSpeed, value);
            foreach(var fuelItem in SelectedGeneratorFuels)
            {
                _calculationService.UpdateFuelModel(fuelItem, _selectedGeneratorClockSpeed);
            }
        }
    }

    public ObservableCollection<GeneratorModel> Generators => _applicationState.Configuration.Generators;

    public GeneratorsViewModel(ApplicationState applicationState, CalculationService calculationService)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
        Notify(nameof(Generators));
    }


    private ApplicationState _applicationState;
    private readonly CalculationService _calculationService;
}