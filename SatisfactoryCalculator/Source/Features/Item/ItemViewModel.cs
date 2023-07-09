namespace SatisfactoryCalculator.Source.Features.Item;

internal class ItemViewModel : ObservableObject
{
    private IItem _selectedItem;
    public IItem SelectedItem
    {
        get => _selectedItem;
        set
        {
            SetProperty(ref _selectedItem, value);

            var entityReference = _applicationState.Configuration.ReferenceDictionary[value.ClassName];
            SelectedItemAsIngredientInRecipes = new(entityReference.RecipeIngredient);
            SelectedItemAsBuildingIngredientInRecipes = new(entityReference.RecipeBuildingIngredient);
            SelectedItemAsProductInRecipes = new(entityReference.RecipeProduct);
            SelectedItemAsFuels = new(entityReference.FuelIngredient.Concat(entityReference.FuelByProduct));
        }
    }

    private ObservableCollection<IRecipe> _selectedItemAsIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedItemAsIngredientInRecipes
    {
        get => _selectedItemAsIngredientInRecipes;
        private set
        {
            SetProperty(ref _selectedItemAsIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<IRecipe> _selectedItemAsBuildingIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedItemAsBuildingIngredientInRecipes
    {
        get => _selectedItemAsBuildingIngredientInRecipes;
        private set
        {
            SetProperty(ref _selectedItemAsBuildingIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }


    private ObservableCollection<IRecipe> _selectedItemAsProductInRecipes = new();
    public ObservableCollection<IRecipe> SelectedItemAsProductInRecipes
    {
        get => _selectedItemAsProductInRecipes;
        private set
        {
            SetProperty(ref _selectedItemAsProductInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<GeneratorFuel> _selectedItemAsFuels = new();
    public ObservableCollection<GeneratorFuel> SelectedItemAsFuels
    {
        get => _selectedItemAsFuels;
        private set
        {
            SetProperty(ref _selectedItemAsFuels, value);
            NotifyMarginUpdates();
        }
    }
    
#pragma warning disable CA1822
    public Thickness ProductsSectionMargin => CalculateMargin(false);
#pragma warning restore CA1822
    public Thickness IngredientsSectionMargin => CalculateMargin(_selectedItemAsProductInRecipes.Count > 0);
    public Thickness BuildingIngredientSectionMargin => CalculateMargin(_selectedItemAsIngredientInRecipes.Count > 0);
    public Thickness FuelsSectionMargin => CalculateMargin(_selectedItemAsBuildingIngredientInRecipes.Count > 0 || _selectedItemAsProductInRecipes.Count > 0);

#pragma warning disable CS8618
    public ItemViewModel(ApplicationState applicationState)
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
        Notify(nameof(BuildingIngredientSectionMargin));
        Notify(nameof(FuelsSectionMargin));
    }

    private readonly ApplicationState _applicationState;
}
