namespace SatisfactoryCalculator.Source.ViewModels;

internal class BuildingViewModel : ObservableObject
{
    private IBuilding? _selectedBuilding;
    public IBuilding? SelectedBuilding
    {
        get => _selectedBuilding;
        set
        {
            SetProperty(ref _selectedBuilding, value);

            SelectedBuildingRecipes = value is not null
                ? new (_applicationState.Configuration.ReferenceDictionary[value.ClassName].RecipeProduct)
                : new();
        }
    }

    private ObservableCollection<IRecipe> _selectedBuildingRecipes = new();
    public ObservableCollection<IRecipe> SelectedBuildingRecipes
    {
        get => _selectedBuildingRecipes;
        private set => SetProperty(ref _selectedBuildingRecipes, value);
    }
    
    public BuildingViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
    }

    private readonly ApplicationState _applicationState;
}
