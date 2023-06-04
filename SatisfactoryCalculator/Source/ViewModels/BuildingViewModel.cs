using Building = SatisfactoryCalculator.Source.Models.Building;
using Recipe = SatisfactoryCalculator.Source.Models.Recipe;

namespace SatisfactoryCalculator.Source.ViewModels;

internal class BuildingViewModel : ObservableObject
{
    private Building? _selectedBuilding;
    public Building? SelectedBuilding
    {
        get => _selectedBuilding;
        set
        {
            SetProperty(ref _selectedBuilding, value);

            SelectedBuildingRecipes = value is not null
                ? new ObservableCollection<Recipe>(_applicationState.Configuration.ReferenceDictionary[value.ClassName].RecipeProduct)
                : new();
        }
    }

    private ObservableCollection<Recipe> _selectedBuildingRecipes = new();
    public ObservableCollection<Recipe> SelectedBuildingRecipes
    {
        get => _selectedBuildingRecipes;
        set => SetProperty(ref _selectedBuildingRecipes, value);
    }

    public ObservableCollection<Building> Buildings => _applicationState.Configuration.Buildings;

    public BuildingViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        Notify(nameof(Buildings));
    }

    private ApplicationState _applicationState;
}
