using Building = SatisfactoryCalculator.Source.Models.Building;
using Consumable = SatisfactoryCalculator.Source.Models.Consumable;
using Equipment = SatisfactoryCalculator.Source.Models.Equipment;
using Generator = SatisfactoryCalculator.Source.Models.Generator;
using Item = SatisfactoryCalculator.Source.Models.Item;
using Recipe = SatisfactoryCalculator.Source.Models.Recipe;

namespace SatisfactoryCalculator.Source.ViewModels;

internal class FilterableEntityViewModel : ObservableObject
{
	public FilterableEntityViewModel(PageService pageService)
	{
		_pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
	}

	private object? _currentPage;
    public object? CurrentPage
	{
		get => _currentPage;
		set => SetProperty(ref _currentPage, value);
	}
    
    private IEntity? _selectedEntity;
    public IEntity? SelectedEntity
    {
	    get => _selectedEntity;
	    set
	    {
		    SetProperty(ref _selectedEntity, value);
		    SwitchToContentPage();
	    }
    }
    
    private ObservableCollection<IEntity> _entities = new();

    public ObservableCollection<IEntity> Entities
    {
	    get => _entities;
	    set => SetProperty(ref _entities, value);
    }

    private void SwitchToContentPage()
    {
	    if (_selectedEntity is null)
	    {
		    CurrentPage = null;
		    return;
	    }

	    switch (_selectedEntity)
	    {
		    case Equipment equipment:
			    var equipmentFetchResult = _pageService.FetchPageWithViewModel<EquipmentPage, EquipmentViewModel>();
			    equipmentFetchResult.Item2.SelectedEquipment = equipment;
			    CurrentPage = equipmentFetchResult.Item1;
			    break;
		    case Consumable consumable:
			    var consumableFetchResult = _pageService.FetchPageWithViewModel<ConsumablePage, ConsumableViewModel>();
			    consumableFetchResult.Item2.SelectedConsumable = consumable;
			    CurrentPage = consumableFetchResult.Item1;
			    break;
		    case Item item:
			    var itemFetchResult = _pageService.FetchPageWithViewModel<ItemPage, ItemViewModel>();
			    itemFetchResult.Item2.SelectedItem = item;
			    CurrentPage = itemFetchResult.Item1;
			    break;
		    case Generator generator:
			    var generatorFetchResult = _pageService.FetchPageWithViewModel<GeneratorPage, GeneratorViewModel>();
			    generatorFetchResult.Item2.SelectedGenerator = generator;
			    CurrentPage = generatorFetchResult.Item1;
			    break;
		    case Building building:
			    var buildingFetchResult = _pageService.FetchPageWithViewModel<BuildingPage, BuildingViewModel>();
			    buildingFetchResult.Item2.SelectedBuilding = building;
			    CurrentPage = buildingFetchResult.Item1;
			    break;
		    case Recipe recipe:
			    var recipeFetchResult = _pageService.FetchPageWithViewModel<RecipePage, RecipeViewModel>();
			    recipeFetchResult.Item2.SelectedRecipe = recipe;
			    CurrentPage = recipeFetchResult.Item1;
			    break;
		    default:
			    throw new NotImplementedException("Page switching not implemented");
	    }
    }

    private readonly PageService _pageService;
}