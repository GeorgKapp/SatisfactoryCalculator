namespace SatisfactoryCalculator.Source.ViewModels;

internal class BuildingsViewModel : ObservableObject
{
    private BuildingModel? _selectedBuilding;
    public BuildingModel? SelectedBuilding
    {
        get => _selectedBuilding;
        set
        {
            SetProperty(ref _selectedBuilding, value);

            SelectedBuildingRecipes = value is not null
                ? new ObservableCollection<RecipeModel>(_applicationState.Configuration.ReferenceDictionary[value.ClassName].RecipeProduct)
                : new();
        }
    }

    private ObservableCollection<RecipeModel> _selectedBuildingRecipes = new();
    public ObservableCollection<RecipeModel> SelectedBuildingRecipes
    {
        get => _selectedBuildingRecipes;
        set => SetProperty(ref _selectedBuildingRecipes, value);
    }

    public ObservableCollection<BuildingModel> Buildings => _applicationState.Configuration.Buildings;

    public BuildingsViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        Notify(nameof(Buildings));
    }

    private ApplicationState _applicationState;
}
