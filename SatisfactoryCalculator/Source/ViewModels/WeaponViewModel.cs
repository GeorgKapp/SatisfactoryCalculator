namespace SatisfactoryCalculator.Source.ViewModels;

internal class WeaponViewModel : ObservableObject
{
    private IWeapon _selectedWeapon;
    public IWeapon SelectedWeapon
    {
        get => _selectedWeapon;
        set
        {
            SetProperty(ref _selectedWeapon, value);

            var entityReference = _applicationState.Configuration.ReferenceDictionary[value.ClassName];
            SelectedWeaponAsIngredientInRecipes = new(entityReference.RecipeIngredient);
            SelectedWeaponAsProductInRecipes = new(entityReference.RecipeProduct);
        }
    }

    private ObservableCollection<IRecipe> _selectedWeaponAsIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedWeaponAsIngredientInRecipes
    {
        get => _selectedWeaponAsIngredientInRecipes;
        private set
        {
            SetProperty(ref _selectedWeaponAsIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<IRecipe> _selectedWeaponAsProductInRecipes = new();
    public ObservableCollection<IRecipe> SelectedWeaponAsProductInRecipes
    {
        get => _selectedWeaponAsProductInRecipes;
        private set
        {
            SetProperty(ref _selectedWeaponAsProductInRecipes, value);
            NotifyMarginUpdates();
        }
    }
    
#pragma warning disable CA1822
    public Thickness ProductsSectionMargin => CalculateMargin(false);
#pragma warning restore CA1822
    public Thickness IngredientsSectionMargin => CalculateMargin(_selectedWeaponAsProductInRecipes.Count > 0);

#pragma warning disable CS8618
    public WeaponViewModel(ApplicationState applicationState)
#pragma warning restore CS8618
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
    }

    private static Thickness CalculateMargin(bool isPreviousControlVisible) => 
        isPreviousControlVisible
            ? new(-10, 50, 0, 0)
            : new Thickness(-10, 0, 0, 0);

    private void NotifyMarginUpdates()
    {
        Notify(nameof(ProductsSectionMargin));
        Notify(nameof(IngredientsSectionMargin));
    }

    private readonly ApplicationState _applicationState;
}