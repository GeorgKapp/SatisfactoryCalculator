namespace SatisfactoryCalculator.Source.ViewModels;

internal class ResourceViewModel : ObservableObject
{
    private IResource _selectedResource;
    public IResource SelectedResource
    {
        get => _selectedResource;
        set
        {
            SetProperty(ref _selectedResource, value);

            var entityReference = _applicationState.Configuration.ReferenceDictionary[value.ClassName];
            SelectedResourceAsBuildingIngredientInRecipes = new(entityReference.RecipeBuildingIngredient);
            SelectedResourceAsIngredientInRecipes = new(entityReference.RecipeIngredient);
            SelectedResourceAsFuels = new(entityReference.FuelIngredient.Concat(entityReference.FuelByProduct));
            SelectedMiners = new(_selectedResource.Miners);
        }
    }

    private ObservableCollection<IRecipe> _selectedResourceAsBuildingIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedResourceAsBuildingIngredientInRecipes
    {
        get => _selectedResourceAsBuildingIngredientInRecipes;
        private set
        {
            SetProperty(ref _selectedResourceAsBuildingIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }


    private ObservableCollection<IRecipe> _selectedResourceAsIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedResourceAsIngredientInRecipes
    {
        get => _selectedResourceAsIngredientInRecipes;
        private set
        {
            SetProperty(ref _selectedResourceAsIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<GeneratorFuel> _selectedResourceAsFuels = new();
    public ObservableCollection<GeneratorFuel> SelectedResourceAsFuels
    {
        get => _selectedResourceAsFuels;
        private set
        {
            SetProperty(ref _selectedResourceAsFuels, value);
            NotifyMarginUpdates();
        }
    }
    
    private ObservableCollection<IMiner> _selectedMiners = new();
    public ObservableCollection<IMiner> SelectedMiners
    {
        get => _selectedMiners;
        private set
        {
            SetProperty(ref _selectedMiners, value);
            NotifyMarginUpdates();
        }
    }
    
    public Thickness IngredientsSectionMargin => CalculateMargin(false);
    public Thickness BuildingIngredientSectionMargin => CalculateMargin(_selectedResourceAsIngredientInRecipes.Count > 0);
    public Thickness FuelsSectionMargin => CalculateMargin(_selectedResourceAsBuildingIngredientInRecipes.Count > 0);
    public Thickness MinersSectionMargin => CalculateMargin(_selectedResourceAsFuels.Count > 0);

    
#pragma warning disable CS8618
    public ResourceViewModel(ApplicationState applicationState)
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
        Notify(nameof(IngredientsSectionMargin));
        Notify(nameof(BuildingIngredientSectionMargin));
        Notify(nameof(FuelsSectionMargin));
        Notify(nameof(FuelsSectionMargin));
    }

    private readonly ApplicationState _applicationState;
}
