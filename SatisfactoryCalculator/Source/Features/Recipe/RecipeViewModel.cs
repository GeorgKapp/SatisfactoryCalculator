namespace SatisfactoryCalculator.Source.Features.Recipe;

internal class RecipeViewModel : ObservableObject
{
    private IRecipe _selectedRecipe;
    public IRecipe SelectedRecipe
    {
        get => _selectedRecipe;
        set => SetProperty(ref _selectedRecipe, value);
    }

    public ObservableCollection<IRecipe> Recipes => _applicationState.Configuration.Recipes;

#pragma warning disable CS8618
    public RecipeViewModel(ApplicationState applicationState)
#pragma warning restore CS8618
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        Notify(nameof(Recipes));
    }

    private readonly ApplicationState _applicationState;
}
