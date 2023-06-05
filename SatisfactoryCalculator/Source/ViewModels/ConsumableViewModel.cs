using Fuel = SatisfactoryCalculator.Source.Models.Fuel;

namespace SatisfactoryCalculator.Source.ViewModels;

internal class ConsumableViewModel : ObservableObject
{
    private IConsumable? _selectedConsumable;
    public IConsumable? SelectedConsumable
    {
        get => _selectedConsumable;
        set
        {
            SetProperty(ref _selectedConsumable, value);
            if (value is null)
            {
                SelectedConsumableAsIngredientInRecipes = new();
                SelectedConsumableAsBuildingIngredientInRecipes = new();
                SelectedConsumableAsProductInRecipes = new();
                SelectedConsumableAsFuels = new();
            }
            else
            {
                var entityReference = _applicationState.Configuration.ReferenceDictionary[value.ClassName];
                SelectedConsumableAsIngredientInRecipes = new(entityReference.RecipeIngredient);
                SelectedConsumableAsBuildingIngredientInRecipes = new(entityReference.RecipeBuildingIngredient);
                SelectedConsumableAsProductInRecipes = new(entityReference.RecipeProduct);
                SelectedConsumableAsFuels = new(entityReference.FuelIngredient.Concat(entityReference.FuelByProduct));
            }
        }
    }

    private ObservableCollection<IRecipe> _selectedConsumableAsIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedConsumableAsIngredientInRecipes
    {
        get => _selectedConsumableAsIngredientInRecipes;
        private set
        {
            SetProperty(ref _selectedConsumableAsIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<IRecipe> _selectedConsumableAsBuildingIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedConsumableAsBuildingIngredientInRecipes
    {
        get => _selectedConsumableAsBuildingIngredientInRecipes;
        private set
        {
            SetProperty(ref _selectedConsumableAsBuildingIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }


    private ObservableCollection<IRecipe> _selectedConsumableAsProductInRecipes = new();
    public ObservableCollection<IRecipe> SelectedConsumableAsProductInRecipes
    {
        get => _selectedConsumableAsProductInRecipes;
        private set
        {
            SetProperty(ref _selectedConsumableAsProductInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<Fuel> _selectedConsumableAsFuels = new();
    public ObservableCollection<Fuel> SelectedConsumableAsFuels
    {
        get => _selectedConsumableAsFuels;
        private set
        {
            SetProperty(ref _selectedConsumableAsFuels, value);
            NotifyMarginUpdates();
        }
    }
    
    public Thickness ProductsSectionMargin => CalculateMargin(false);
    public Thickness IngredientsSectionMargin => CalculateMargin(_selectedConsumableAsProductInRecipes.Count > 0);
    public Thickness BuildingIngredientSectionMargin => CalculateMargin(_selectedConsumableAsIngredientInRecipes.Count > 0);
    public Thickness FuelsSectionMargin => CalculateMargin(_selectedConsumableAsBuildingIngredientInRecipes.Count > 0 || _selectedConsumableAsProductInRecipes.Count > 0);

    public ConsumableViewModel(ApplicationState applicationState)
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