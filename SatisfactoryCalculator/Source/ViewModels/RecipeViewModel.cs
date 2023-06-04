using Recipe = SatisfactoryCalculator.Source.Models.Recipe;

namespace SatisfactoryCalculator.Source.ViewModels;

internal class RecipeViewModel : ObservableObject
{
    private Recipe? _selectedRecipe;
    public Recipe? SelectedRecipe
    {
        get => _selectedRecipe;
        set => SetProperty(ref _selectedRecipe, value);
    }

    public ObservableCollection<Recipe> Recipes => _applicationState.Configuration.Recipes;

    public RecipeViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        Notify(nameof(Recipes));
    }

    private ApplicationState _applicationState;
}
