using SatisfactoryCalculator.Source.Features.Shared.FilterableEntity;
using SatisfactoryCalculator.Source.Features.Shared.Models;

namespace SatisfactoryCalculator.Source.Features.Main;

internal class MainViewModel : ObservableObject
{
	private object? _currentPage;
    public object? CurrentPage
	{
		get => _currentPage;
		private set => SetProperty(ref _currentPage, value);
	}

    private bool _isInitializing;
    public bool IsInitializing
    {
        get => _isInitializing;
        set => SetProperty(ref _isInitializing, value);
    }

    private string? _initializingText;
    public string? InitializingText
    {
        get => _initializingText;
        set => SetProperty(ref _initializingText, value);
    }

    private ICommand? _showItemsCommand;
    public ICommand ShowItemsCommand => _showItemsCommand ??= new SimpleCommand(ShowItems);
    
    private ICommand? _showEquipmentsCommand;
    public ICommand ShowEquipmentsCommand => _showEquipmentsCommand ??= new SimpleCommand(ShowEquipments);
    
    private ICommand? _showConsumablesCommand;
    public ICommand ShowConsumablesCommand => _showConsumablesCommand ??= new SimpleCommand(ShowConsumables);
    
    private ICommand? _showWeaponsCommand;
    public ICommand ShowWeaponsCommand => _showWeaponsCommand ??= new SimpleCommand(ShowWeapons);
    
    private ICommand? _showAmmunitionsCommand;
    public ICommand ShowAmmunitionsCommand => _showAmmunitionsCommand ??= new SimpleCommand(ShowAmmunitions);
    
    private ICommand? _showResourcesCommand;
    public ICommand ShowRecourcesCommand => _showResourcesCommand ??= new SimpleCommand(ShowRecources);

    private ICommand? _showBuildingsCommand;
    public ICommand ShowBuildingsCommand => _showBuildingsCommand ??= new SimpleCommand(ShowBuildings);

    private ICommand? _showGeneratorsCommand;
    public ICommand ShowGeneratorsCommand => _showGeneratorsCommand ??= new SimpleCommand(ShowGenerators);
    
    private ICommand? _showMinersCommand;
    public ICommand ShowMinersCommand => _showMinersCommand ??= new SimpleCommand(ShowMiners);
    
    private ICommand? _showStatuesCommand;
    public ICommand ShowStatuesCommand => _showStatuesCommand ??= new SimpleCommand(ShowStatues);

    private ICommand? _showRecipesCommand;
    public ICommand ShowRecipesCommand => _showRecipesCommand ??= new SimpleCommand(ShowRecipes);

    private ICommand? _showOverviewCommand;
    public ICommand ShowOverviewCommand => _showOverviewCommand ??= new SimpleCommand(ShowOverview);

    private ICommand? _showFactoryPlannerCommand;
    public ICommand ShowFactoryPlannerCommand => _showFactoryPlannerCommand ??= new SimpleCommand(ShowFactoryPlanner);

    private ICommand? _showDataImportCommand;
    public ICommand ShowDataImportCommand => _showDataImportCommand ??= new SimpleCommand(ShowDataImport);
    
	public MainViewModel(
		IDbContextFactory<ModelContext> modelContextFactory,
		ApplicationState applicationState, 
		JsonService jsonService, 
		DataModelMappingService dataModelMappingService)
	{
		_modelContextFactory = modelContextFactory ?? throw new ArgumentNullException(nameof(modelContextFactory));
		_applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
		_jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
		_dataModelMappingService = dataModelMappingService ?? throw new ArgumentNullException(nameof(dataModelMappingService));
    }

    private void ShowItems() => ShowFilterableEntityPage(_applicationState.Configuration.Items);
    private void ShowEquipments() => ShowFilterableEntityPage(_applicationState.Configuration.Equipments);
    private void ShowConsumables() => ShowFilterableEntityPage(_applicationState.Configuration.Consumables);
    private void ShowWeapons() => ShowFilterableEntityPage(_applicationState.Configuration.Weapons);
    private void ShowAmmunitions() => ShowFilterableEntityPage(_applicationState.Configuration.Ammunitions);
    private void ShowRecources() => ShowFilterableEntityPage(_applicationState.Configuration.Resources);
    private void ShowBuildings() => ShowFilterableEntityPage(_applicationState.Configuration.Buildings);
    private void ShowGenerators() => ShowFilterableEntityPage(_applicationState.Configuration.Generators);
    private void ShowMiners() => ShowFilterableEntityPage(_applicationState.Configuration.Miners);
    private void ShowStatues() => ShowFilterableEntityPage(_applicationState.Configuration.Statues);
    private void ShowRecipes() => ShowFilterableEntityPage(_applicationState.Configuration.Recipes);
    private void ShowOverview()
    {
	    var result = PageService.FetchPageWithViewModel<OverviewPage, OverviewViewModel>();
	    result.Item2.Update();
	    CurrentPage = result.Item1;
    }
    private void ShowFactoryPlanner()
    {
	    var fetchResult = PageService.FetchPageWithViewModel<FactoryPlannerPage, FactoryPlannerViewModel>();
	    fetchResult.Item2.Initialize();
	    CurrentPage = fetchResult.Item1;
    }

    private void ShowDataImport() => CurrentPage = PageService.FetchPage<DataImportPage>();

	private void ShowFilterableEntityPage(IEnumerable<IEntity> entities)
	{
		var fetchResult = PageService.FetchPageWithViewModel<FilterableEntityPage, FilterableEntityViewModel>();
		fetchResult.Item2.Entities = new(entities);
		CurrentPage = fetchResult.Item1;
	}
	
	public async Task Load()
    {
	    try
	    {
		    InitializingText = "Initializing";
		    IsInitializing = true;
		    
		    var mappedData = await _dataModelMappingService.MapConfigurationModelsAsync();
		    _applicationState.SetConfig(mappedData);
	    }
	    catch (Exception exception)
	    {
		    InitializingText = "Exception occured: Exception copied to clipboard";
		    ClipBoardService.CopyToClipboard(exception.Message);
		    await Task.Delay(1000);
	    }

	    finally
	    {
		    InitializingText = string.Empty;
		    IsInitializing = false;
	    }
    }

    private readonly IDbContextFactory<ModelContext> _modelContextFactory;
    private readonly ApplicationState _applicationState;
    private readonly JsonService _jsonService;
    private readonly DataModelMappingService _dataModelMappingService;
}