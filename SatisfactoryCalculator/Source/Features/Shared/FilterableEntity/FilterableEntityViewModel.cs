namespace SatisfactoryCalculator.Source.Features.Shared.FilterableEntity;

internal class FilterableEntityViewModel : ObservableObject
{
	private object? _currentPage;
    public object? CurrentPage
	{
		get => _currentPage;
		private set => SetProperty(ref _currentPage, value);
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
		    case Equipment.Equipment equipment:
			    var equipmentFetchResult = PageService.FetchPageWithViewModel<EquipmentPage, EquipmentViewModel>();
			    equipmentFetchResult.Item2.SelectedEquipment = equipment;
			    CurrentPage = equipmentFetchResult.Item1;
			    break;
		    case Consumable.Consumable consumable:
			    var consumableFetchResult = PageService.FetchPageWithViewModel<ConsumablePage, ConsumableViewModel>();
			    consumableFetchResult.Item2.SelectedConsumable = consumable;
			    CurrentPage = consumableFetchResult.Item1;
			    break;
		    case Weapon.Weapon weapon:
			    var weaponFetchResult = PageService.FetchPageWithViewModel<WeaponPage, WeaponViewModel>();
			    weaponFetchResult.Item2.SelectedWeapon = weapon;
			    CurrentPage = weaponFetchResult.Item1;
			    break;
		    case Ammunition.Ammunition ammunition:
			    var ammunitionFetchResult = PageService.FetchPageWithViewModel<AmmunitionPage, AmmunitionViewModel>();
			    ammunitionFetchResult.Item2.SelectedAmmunition = ammunition;
			    CurrentPage = ammunitionFetchResult.Item1;
			    break;
		    case Resource.Resource resource:
			    var resourceFetchResult = PageService.FetchPageWithViewModel<ResourcePage, ResourceViewModel>();
			    resourceFetchResult.Item2.SelectedResource = resource;
			    CurrentPage = resourceFetchResult.Item1;
			    break;
		    case Item.Item item:
			    var itemFetchResult = PageService.FetchPageWithViewModel<ItemPage, ItemViewModel>();
			    itemFetchResult.Item2.SelectedItem = item;
			    CurrentPage = itemFetchResult.Item1;
			    break;
		    case Generator.Generator generator:
			    var generatorFetchResult = PageService.FetchPageWithViewModel<GeneratorPage, GeneratorViewModel>();
			    generatorFetchResult.Item2.SelectedGenerator = generator;
			    CurrentPage = generatorFetchResult.Item1;
			    break;
		    case Miner.Miner miner:
			    var minerFetchResult = PageService.FetchPageWithViewModel<MinerPage, MinerViewModel>();
			    minerFetchResult.Item2.SelectedMiner = miner;
			    CurrentPage = minerFetchResult.Item1;
			    break;
		    case Building.Building building:
			    var buildingFetchResult = PageService.FetchPageWithViewModel<BuildingPage, BuildingViewModel>();
			    buildingFetchResult.Item2.SelectedBuilding = building;
			    CurrentPage = buildingFetchResult.Item1;
			    break;
		    case Source.Models.Statue statue:
			    var statueFetchResult = PageService.FetchPageWithViewModel<StatuePage, StatueViewModel>();
			    statueFetchResult.Item2.SelectedStatue = statue;
			    CurrentPage = statueFetchResult.Item1;
			    break;
		    case Recipe.Recipe recipe:
			    var recipeFetchResult = PageService.FetchPageWithViewModel<RecipePage, RecipeViewModel>();
			    recipeFetchResult.Item2.SelectedRecipe = recipe;
			    CurrentPage = recipeFetchResult.Item1;
			    break;
		    default:
			    // ReSharper disable once NotResolvedInText
			    throw new ArgumentOutOfRangeException("Page switching not implemented");
	    }
    }
}