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

            if(value is null)
            {
                SelectedGeneratorFuels = new();
                SelectedGeneratorRecipes = new();
            }
            else
            {
                SelectedGeneratorFuels = new ObservableCollection<FuelModel>(_applicationState.Configuration.ReferenceDictionary[value.ClassName].FuelGenerator);
                SelectedGeneratorRecipes = new ObservableCollection<RecipeModel>(_applicationState.Configuration.ReferenceDictionary[value.ClassName].RecipeProduct);
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

    public ObservableCollection<GeneratorModel> Generators => _applicationState.Configuration.Generators;

    public GeneratorsViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        Notify(nameof(Generators));
    }

    private ApplicationState _applicationState;
}