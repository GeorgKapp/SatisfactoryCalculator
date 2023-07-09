namespace SatisfactoryCalculator.Source.Features.Ammunition;

internal class AmmunitionViewModel : ObservableObject
{
    private IAmmunition _selectedAmmunition;
    public IAmmunition SelectedAmmunition
    {
        get => _selectedAmmunition;
        set
        {
            SetProperty(ref _selectedAmmunition, value);

            var entityReference = _applicationState.Configuration.ReferenceDictionary[value.ClassName];
            SelectedAmmunitionAsIngredientInRecipes = new(entityReference.RecipeIngredient);
            SelectedAmmunitionAsBuildingIngredientInRecipes = new(entityReference.RecipeBuildingIngredient);
            SelectedAmmunitionAsProductInRecipes = new(entityReference.RecipeProduct);
            SelectedAmmunitionAsFuels = new(entityReference.FuelIngredient.Concat(entityReference.FuelByProduct));
        }
    }

    private ObservableCollection<IRecipe> _selectedAmmunitionAsIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedAmmunitionAsIngredientInRecipes
    {
        get => _selectedAmmunitionAsIngredientInRecipes;
        private set
        {
            SetProperty(ref _selectedAmmunitionAsIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<IRecipe> _selectedAmmunitionAsBuildingIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedAmmunitionAsBuildingIngredientInRecipes
    {
        get => _selectedAmmunitionAsBuildingIngredientInRecipes;
        private set
        {
            SetProperty(ref _selectedAmmunitionAsBuildingIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }


    private ObservableCollection<IRecipe> _selectedAmmunitionAsProductInRecipes = new();
    public ObservableCollection<IRecipe> SelectedAmmunitionAsProductInRecipes
    {
        get => _selectedAmmunitionAsProductInRecipes;
        private set
        {
            SetProperty(ref _selectedAmmunitionAsProductInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<GeneratorFuel> _selectedAmmunitionAsFuels = new();
    public ObservableCollection<GeneratorFuel> SelectedAmmunitionAsFuels
    {
        get => _selectedAmmunitionAsFuels;
        private set
        {
            SetProperty(ref _selectedAmmunitionAsFuels, value);
            NotifyMarginUpdates();
        }
    }
    
#pragma warning disable CA1822
    public Thickness ProductsSectionMargin => CalculateMargin(false);
#pragma warning restore CA1822
    public Thickness IngredientsSectionMargin => CalculateMargin(_selectedAmmunitionAsProductInRecipes.Count > 0);
    public Thickness BuildingIngredientSectionMargin => CalculateMargin(_selectedAmmunitionAsIngredientInRecipes.Count > 0);
    public Thickness FuelsSectionMargin => CalculateMargin(_selectedAmmunitionAsBuildingIngredientInRecipes.Count > 0 || _selectedAmmunitionAsProductInRecipes.Count > 0);

#pragma warning disable CS8618
    public AmmunitionViewModel(ApplicationState applicationState)
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