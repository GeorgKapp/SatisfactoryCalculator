namespace SatisfactoryCalculator.Source.ViewModels;

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

    private ICommand? _showBuildingsCommand;
    public ICommand ShowBuildingsCommand => _showBuildingsCommand ??= new SimpleCommand(ShowBuildings);

    private ICommand? _showGeneratorsCommand;
    public ICommand ShowGeneratorsCommand => _showGeneratorsCommand ??= new SimpleCommand(ShowGenerators);

    private ICommand? _showRecipesCommand;
    public ICommand ShowRecipesCommand => _showRecipesCommand ??= new SimpleCommand(ShowRecipes);

    private ICommand? _showOverviewCommand;
    public ICommand ShowOverviewCommand => _showOverviewCommand ??= new SimpleCommand(ShowOverview);

    private ICommand? _showDataImportCommand;
    public ICommand ShowDataImportCommand => _showDataImportCommand ??= new SimpleCommand(ShowDataImport);

    private ICommand? _saveCommand;
    public ICommand SaveCommand => _saveCommand ??= new SimpleCommand(Save);

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
    private void ShowBuildings() => ShowFilterableEntityPage(_applicationState.Configuration.Buildings);
    private void ShowGenerators() => ShowFilterableEntityPage(_applicationState.Configuration.Generators);
    private void ShowRecipes() => ShowFilterableEntityPage(_applicationState.Configuration.Recipes);
    private void ShowOverview()
    {
	    var result = PageService.FetchPageWithViewModel<OverviewPage, OverviewViewModel>();
	    result.Item2.Update();
	    CurrentPage = result.Item1;
    }
    private void ShowDataImport() => CurrentPage = PageService.FetchPage<DataImportPage>();

	private void ShowFilterableEntityPage(IEnumerable<IEntity> entities)
	{
		var fetchResult = PageService.FetchPageWithViewModel<FilterableEntityPage, FilterableEntityViewModel>();
		fetchResult.Item2.Entities = new(entities);
		CurrentPage = fetchResult.Item1;
	}
	
	private void Save()
	{
		//Insert instance saving maybe
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