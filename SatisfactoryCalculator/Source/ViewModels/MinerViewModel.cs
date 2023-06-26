namespace SatisfactoryCalculator.Source.ViewModels;

internal class MinerViewModel : ObservableObject
{
    private IMiner _selectedMiner;
    public IMiner SelectedMiner
    {
        get => _selectedMiner;
        set
        {
            SetProperty(ref _selectedMiner, value);
            
            SelectedMinerRecipes = new(_applicationState.Configuration.ReferenceDictionary[value.ClassName].RecipeProduct);
            SelectedResources = new(_selectedMiner.Resources);
        }
    }

    private ObservableCollection<IRecipe> _selectedMinerRecipes = new();
    public ObservableCollection<IRecipe> SelectedMinerRecipes
    {
        get => _selectedMinerRecipes;
        private set => SetProperty(ref _selectedMinerRecipes, value);
    }
    
    private ObservableCollection<IResource> _selectedResources = new();
    public ObservableCollection<IResource> SelectedResources
    {
        get => _selectedResources;
        private set
        {
            SetProperty(ref _selectedResources, value);
            NotifyMarginUpdates();
        }
    }
    
    public Thickness ResourcesSectionMargin => CalculateMargin(_selectedMinerRecipes.Count > 0);
    
#pragma warning disable CS8618
    public MinerViewModel(ApplicationState applicationState)
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
        Notify(nameof(ResourcesSectionMargin));
    }

    private readonly ApplicationState _applicationState;
}