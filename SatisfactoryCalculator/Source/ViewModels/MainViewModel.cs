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

    private ICommand _showBuildingsCommand;
    public ICommand ShowBuildingsCommand => _showBuildingsCommand ??= new SimpleCommand(new Action(ShowBuildings));

    private ICommand _showItemsCommand;
    public ICommand ShowItemsCommand => _showItemsCommand ??= new SimpleCommand(new Action(ShowItems));

    private ICommand _showRecipesCommand;
    public ICommand ShowRecipesCommand => _showRecipesCommand ??= new SimpleCommand(new Action(ShowRecipes));

    private ICommand _showOverviewCommand;
    public ICommand ShowOverviewCommand => _showOverviewCommand ??= new SimpleCommand(new Action(ShowOverview));

    private ICommand _showDataImportCommand;
    public ICommand ShowDataImportCommand => _showDataImportCommand ??= new SimpleCommand(new Action(ShowDataImport));

    private ICommand _saveCommand;
    public ICommand SaveCommand => _saveCommand ??= new SimpleCommand(new Action(Save));

	public MainViewModel(ApplicationState applicationState, JsonService jsonService, ViewModelViewLinker viewModelViewLinker, DataModelMappingService dataModelMappingService)
	{
		_applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
		_jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
		_viewModelViewLinker = viewModelViewLinker ?? throw new ArgumentNullException(nameof(viewModelViewLinker));
        _dataModelMappingService = dataModelMappingService ?? throw new ArgumentNullException(nameof(dataModelMappingService));
    }

	private void ShowBuildings() => CurrentPage = _viewModelViewLinker.GetPageByType<BuildingsPage>();
	private void ShowItems() => CurrentPage = _viewModelViewLinker.GetPageByType<ItemsPage>();
	private void ShowRecipes() => CurrentPage = _viewModelViewLinker.GetPageByType<RecipesPage>();
	private void ShowOverview() => CurrentPage = _viewModelViewLinker.GetPageByType<OverviewPage>();
	private void ShowDataImport() => CurrentPage = _viewModelViewLinker.GetPageByType<DataImportPage>();

	private void Save()
	{
		Constants.DataFilePath.CreateDirectoryIfNotExists();
		_jsonService.WriteJson(_applicationState.Data, Constants.InformationFileName);
	}

    public async Task Load()
    {
        IsInitializing = true;
        InitializingText = "Initializing";

        var data = await _jsonService.ReadJsonAsync<Data>(Constants.InformationFileName);
        var mappedData = _dataModelMappingService.MapToConfigurationModel(data);

        _applicationState.SetConfig(data, mappedData);

        IsInitializing = false;
        InitializingText = string.Empty;
    }

    private ApplicationState _applicationState;

    private readonly JsonService _jsonService;
    private readonly ViewModelViewLinker _viewModelViewLinker;
    private readonly DataModelMappingService _dataModelMappingService;
}
