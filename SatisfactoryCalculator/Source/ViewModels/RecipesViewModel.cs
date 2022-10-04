namespace SatisfactoryCalculator.Source.ViewModels;

internal class RecipesViewModel : ObservableObject
{
    private RecipeModel? _selectedRecipe;
    public RecipeModel? SelectedRecipe
    {
        get => _selectedRecipe;
        set => SetProperty(ref _selectedRecipe, value);
    }

    public ObservableCollection<RecipeModel> Recipes => _applicationState.Configuration.Recipes;

    public RecipesViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        Notify(nameof(Recipes));
    }

    private ApplicationState _applicationState;
}
