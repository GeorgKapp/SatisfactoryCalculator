namespace SatisfactoryCalculator.Source.ViewModels;

internal class EquipmentViewModel : ObservableObject
{
    private IEquipment? _selectedEquipment;
    public IEquipment? SelectedEquipment
    {
        get => _selectedEquipment;
        set
        {
            SetProperty(ref _selectedEquipment, value);
            if (value is null)
            {
                SelectedEquipmentAsIngredientInRecipes = new();
                SelectedEquipmentAsProductInRecipes = new();
            }
            else
            {
                var entityReference = _applicationState.Configuration.ReferenceDictionary[value.ClassName];
                SelectedEquipmentAsIngredientInRecipes = new(entityReference.RecipeIngredient);
                SelectedEquipmentAsProductInRecipes = new(entityReference.RecipeProduct);
            }
        }
    }

    private ObservableCollection<IRecipe> _selectedEquipmentAsIngredientInRecipes = new();
    public ObservableCollection<IRecipe> SelectedEquipmentAsIngredientInRecipes
    {
        get => _selectedEquipmentAsIngredientInRecipes;
        private set
        {
            SetProperty(ref _selectedEquipmentAsIngredientInRecipes, value);
            NotifyMarginUpdates();
        }
    }

    private ObservableCollection<IRecipe> _selectedEquipmentAsProductInRecipes = new();
    public ObservableCollection<IRecipe> SelectedEquipmentAsProductInRecipes
    {
        get => _selectedEquipmentAsProductInRecipes;
        private set
        {
            SetProperty(ref _selectedEquipmentAsProductInRecipes, value);
            NotifyMarginUpdates();
        }
    }
    
    public Thickness ProductsSectionMargin => CalculateMargin(false);
    public Thickness IngredientsSectionMargin => CalculateMargin(_selectedEquipmentAsProductInRecipes.Count > 0);
    
    public EquipmentViewModel(ApplicationState applicationState)
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
