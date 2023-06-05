using Fuel = SatisfactoryCalculator.Source.Models.Fuel;

namespace SatisfactoryCalculator.Source.ViewModels;

internal class ItemViewModel : ObservableObject
{
    private IItem? _selectedItem;
    public IItem? SelectedItem
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
                SelectedItemAsFuels = new();
            }
            else
            {
                var entityReference = _applicationState.Configuration.ReferenceDictionary[value.ClassName];
                SelectedItemAsIngredientInRecipes = new(entityReference.RecipeIngredient);
                SelectedItemAsBuildingIngredientInRecipes = new(entityReference.RecipeBuildingIngredient);
                SelectedItemAsProductInRecipes = new(entityReference.RecipeProduct);
                SelectedItemAsFuels = new(entityReference.FuelIngredient.Concat(entityReference.FuelByProduct));
            }
        }
    }

    private ObservableCollection<IRecipe> _selectedItemAsIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedItemAsIngredientInRecipes
    {
        get => _selectedItemAsIngredientInRecipes;
        set
        {
            SetProperty(ref _selectedItemAsIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<IRecipe> _selectedItemAsBuildingIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedItemAsBuildingIngredientInRecipes
    {
        get => _selectedItemAsBuildingIngredientInRecipes;
        set
        {
            SetProperty(ref _selectedItemAsBuildingIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }


    private ObservableCollection<IRecipe> _selectedItemAsProductInRecipes = new();
    public ObservableCollection<IRecipe> SelectedItemAsProductInRecipes
    {
        get => _selectedItemAsProductInRecipes;
        set
        {
            SetProperty(ref _selectedItemAsProductInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<Fuel> _selectedItemAsFuels = new();
    public ObservableCollection<Fuel> SelectedItemAsFuels
    {
        get => _selectedItemAsFuels;
        set
        {
            SetProperty(ref _selectedItemAsFuels, value);
            NotifyMarginUpdates();
        }
    }
    
    public Thickness ProductsSectionMargin => CalculateMargin(false);
    public Thickness IngredientsSectionMargin => CalculateMargin(_selectedItemAsProductInRecipes.Count > 0);
    public Thickness BuildingIngredientSectionMargin => CalculateMargin(_selectedItemAsIngredientInRecipes.Count > 0);
    public Thickness FuelsSectionMargin => CalculateMargin(_selectedItemAsBuildingIngredientInRecipes.Count > 0 || _selectedItemAsProductInRecipes.Count > 0);

    public ItemViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
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
        Notify(nameof(FuelsSectionMargin));
    }

    private ApplicationState _applicationState;
}
