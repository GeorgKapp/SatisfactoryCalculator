namespace SatisfactoryCalculator.Source.ViewModels;

internal class RecipeViewModel : ObservableObject
{
    private RecipeModel? _selectedRecipe;
    public RecipeModel? SelectedRecipe
    {
        get => _selectedRecipe;
        set => SetProperty(ref _selectedRecipe, value);
    }

    public ObservableCollection<RecipeModel> Recipes => _applicationState.Configuration.Recipes;

    public RecipeViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        Notify(nameof(Recipes));
    }

    private ApplicationState _applicationState;
}
