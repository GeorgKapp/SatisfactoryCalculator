namespace SatisfactoryCalculator.Source.ViewModels;

internal class ConsumableViewModel : ObservableObject
{
    private IConsumable _selectedConsumable;
    public IConsumable SelectedConsumable
    {
        get => _selectedConsumable;
        set
        {
            SetProperty(ref _selectedConsumable, value);

            var entityReference = _applicationState.Configuration.ReferenceDictionary[value.ClassName];
            SelectedConsumableAsIngredientInRecipes = new(entityReference.RecipeIngredient);
            SelectedConsumableAsBuildingIngredientInRecipes = new(entityReference.RecipeBuildingIngredient);
            SelectedConsumableAsProductInRecipes = new(entityReference.RecipeProduct);
            SelectedConsumableAsFuels = new(entityReference.FuelIngredient.Concat(entityReference.FuelByProduct));
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

    private ObservableCollection<GeneratorFuel> _selectedConsumableAsFuels = new();
    public ObservableCollection<GeneratorFuel> SelectedConsumableAsFuels
    {
        get => _selectedConsumableAsFuels;
        private set
        {
            SetProperty(ref _selectedConsumableAsFuels, value);
            NotifyMarginUpdates();
        }
    }
    
#pragma warning disable CA1822
    public Thickness ProductsSectionMargin => CalculateMargin(false);
#pragma warning restore CA1822
    public Thickness IngredientsSectionMargin => CalculateMargin(_selectedConsumableAsProductInRecipes.Count > 0);
    public Thickness BuildingIngredientSectionMargin => CalculateMargin(_selectedConsumableAsIngredientInRecipes.Count > 0);
    public Thickness FuelsSectionMargin => CalculateMargin(_selectedConsumableAsBuildingIngredientInRecipes.Count > 0 || _selectedConsumableAsProductInRecipes.Count > 0);

#pragma warning disable CS8618
    public ConsumableViewModel(ApplicationState applicationState)
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