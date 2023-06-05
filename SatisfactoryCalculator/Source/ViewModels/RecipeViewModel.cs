namespace SatisfactoryCalculator.Source.ViewModels;

internal class RecipeViewModel : ObservableObject
{
    private IRecipe? _selectedRecipe;
    public IRecipe? SelectedRecipe
    {
        get => _selectedRecipe;
        set => SetProperty(ref _selectedRecipe, value);
    }

    public ObservableCollection<IRecipe> Recipes => _applicationState.Configuration.Recipes;

    public RecipeViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        Notify(nameof(Recipes));
    }

    private readonly ApplicationState _applicationState;
}
