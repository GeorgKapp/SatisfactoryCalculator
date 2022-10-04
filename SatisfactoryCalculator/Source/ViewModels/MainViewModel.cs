namespace SatisfactoryCalculator.Source.ViewModels;

internal class MainViewModel : ObservableObject
{
    private object _currentPage;
    public object CurrentPage
	{
		get => _currentPage;
		set => SetProperty(ref _currentPage, value);
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

	public MainViewModel(ApplicationState applicationState, JsonService jsonService, ViewModelViewLinker viewModelViewLinker)
	{
		_applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
		_jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
		_viewModelViewLinker = viewModelViewLinker ?? throw new ArgumentNullException(nameof(viewModelViewLinker));
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

    private ApplicationState _applicationState;

    private readonly JsonService _jsonService;
    private readonly ViewModelViewLinker _viewModelViewLinker;
}
