namespace SatisfactoryCalculator.Source.ViewModels;

internal class ItemsViewModel : ObservableObject
{
    private ItemModel? _selectedItem;
    public ItemModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            SetProperty(ref _selectedItem, value);
            if (value is null)
            {
                SelectedItemAsIngredientInRecipes = new();
                SelectedItemAsBuildingIngredientInRecipes = new();
                SelectedItemAsProductInRecipes = new();
            }
            else
            {
                var entityReference = _applicationState.Configuration.ReferenceDictionary[value.ClassName];
                SelectedItemAsIngredientInRecipes = new ObservableCollection<RecipeModel>(entityReference.RecipeIngredient);
                SelectedItemAsBuildingIngredientInRecipes = new ObservableCollection<RecipeModel>(entityReference.RecipeBuildingIngredient);
                SelectedItemAsProductInRecipes = new ObservableCollection<RecipeModel>(entityReference.RecipeProduct);
            }
        }
    }

    private ObservableCollection<RecipeModel> _selectedItemAsIngredientInRecipes = new();
    public ObservableCollection<RecipeModel> SelectedItemAsIngredientInRecipes
    {
        get => _selectedItemAsIngredientInRecipes;
        set
        {
            SetProperty(ref _selectedItemAsIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<RecipeModel> _selectedItemAsBuildingIngredientInRecipes = new();
    public ObservableCollection<RecipeModel> SelectedItemAsBuildingIngredientInRecipes
    {
        get => _selectedItemAsBuildingIngredientInRecipes;
        set
        {
            SetProperty(ref _selectedItemAsBuildingIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }


    private ObservableCollection<RecipeModel> _selectedItemAsProductInRecipes = new();
    public ObservableCollection<RecipeModel> SelectedItemAsProductInRecipes
    {
        get => _selectedItemAsProductInRecipes;
        set
        {
            SetProperty(ref _selectedItemAsProductInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    public ObservableCollection<ItemModel> Items => _applicationState.Configuration.Items;

    public Thickness ProductsSectionMargin => CalculateMargin(false);
    public Thickness IngredientsSectionMargin => CalculateMargin(_selectedItemAsProductInRecipes.Count > 0);
    public Thickness BuildingIngredientSectionMargin => CalculateMargin(_selectedItemAsIngredientInRecipes.Count > 0);

    public ItemsViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        Notify(nameof(Items));
    }

    private Thickness CalculateMargin(bool isPreviousControlVisible) => 
        isPreviousControlVisible
            ? new Thickness(-10, 50, 0, 0)
            : new Thickness(-10, 0, 0, 0);

    private void NotifyMarginUpdates()
    {
        Notify(nameof(ProductsSectionMargin));
        Notify(nameof(IngredientsSectionMargin));
        Notify(nameof(BuildingIngredientSectionMargin));
    }

    private ApplicationState _applicationState;
}
