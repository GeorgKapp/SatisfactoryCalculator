namespace SatisfactoryCalculator.Source.Features.Building;

internal class BuildingViewModel : ObservableObject
{
    private IBuilding _selectedBuilding;
    public IBuilding SelectedBuilding
    {
        get => _selectedBuilding;
        set
        {
            SetProperty(ref _selectedBuilding, value);
            
            SelectedBuildingRecipes = new(_applicationState.Configuration.ReferenceDictionary[value.ClassName].RecipeProduct);
        }
    }

    private ObservableCollection<IRecipe> _selectedBuildingRecipes = new();
    public ObservableCollection<IRecipe> SelectedBuildingRecipes
    {
        get => _selectedBuildingRecipes;
        private set => SetProperty(ref _selectedBuildingRecipes, value);
    }
    
#pragma warning disable CS8618
    public BuildingViewModel(ApplicationState applicationState)
#pragma warning restore CS8618
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
    }

    private readonly ApplicationState _applicationState;
}
