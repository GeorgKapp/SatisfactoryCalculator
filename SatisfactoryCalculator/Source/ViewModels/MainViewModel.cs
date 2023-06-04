namespace SatisfactoryCalculator.Source.ViewModels;

internal class MainViewModel : ObservableObject
{
    private object _currentPage;
    public object CurrentPage
	{
		get => _currentPage;
		set => SetProperty(ref _currentPage, value);
	}

    private bool _isInitializing;
    public bool IsInitializing
    {
        get => _isInitializing;
        set => SetProperty(ref _isInitializing, value);
    }

    private string _initializingText;
    public string InitializingText
    {
        get => _initializingText;
        set => SetProperty(ref _initializingText, value);
    }

    private ICommand? _showItemsCommand;
    public ICommand ShowItemsCommand => _showItemsCommand ??= new SimpleCommand(ShowItems);

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

	public MainViewModel(ApplicationState applicationState, JsonService jsonService, ViewModelViewLinker viewModelViewLinker, DataModelMappingService dataModelMappingService)
	{
		_applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
		_jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
		_viewModelViewLinker = viewModelViewLinker ?? throw new ArgumentNullException(nameof(viewModelViewLinker));
        _dataModelMappingService = dataModelMappingService ?? throw new ArgumentNullException(nameof(dataModelMappingService));
    }

    private void ShowItems() => CurrentPage = _viewModelViewLinker.GetPageByType<ItemPage>();
    private void ShowBuildings() => CurrentPage = _viewModelViewLinker.GetPageByType<BuildingPage>();
    private void ShowGenerators() => CurrentPage = _viewModelViewLinker.GetPageByType<GeneratorPage>();
    private void ShowRecipes() => CurrentPage = _viewModelViewLinker.GetPageByType<RecipePage>();
	private void ShowOverview() => CurrentPage = _viewModelViewLinker.GetPageByType<OverviewPage>();
	private void ShowDataImport() => CurrentPage = _viewModelViewLinker.GetPageByType<DataImportPage>();

	private void Save()
	{
		if (!Directory.Exists(Constants.DataFilePath))
			Directory.CreateDirectory(Constants.DataFilePath);
		
		_jsonService.WriteJson(_applicationState.Data, Constants.InformationFileName);
	}

    public async Task Load()
    {
        InitializingText = "Initializing";
        IsInitializing = true;

        var data = await DebugExtensions.ProfileAsync(
            _jsonService.ReadUtf8JsonAsync<Data>(Constants.InformationFileName), 
            "Read Data");

        var mappedData = DebugExtensions.Profile(() =>
            _dataModelMappingService.MapToConfigurationModel(data),
            "Map Data");

        _applicationState.SetConfig(data, mappedData);

        IsInitializing = false;
        InitializingText = string.Empty;
    }

    private ApplicationState _applicationState;

    private readonly JsonService _jsonService;
    private readonly ViewModelViewLinker _viewModelViewLinker;
    private readonly DataModelMappingService _dataModelMappingService;
}