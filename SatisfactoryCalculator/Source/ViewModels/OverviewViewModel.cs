namespace SatisfactoryCalculator.Source.ViewModels;

internal class OverviewViewModel : ObservableObject
{
	public int BuildingAmount => _applicationState.Configuration.Buildings.Count;
	public int ItemAmount => _applicationState.Configuration.Items.Count;
	public int RecipeAmount => _applicationState.Configuration.Recipes.Count;
	public DateTime? LastSyncDate => _applicationState.Configuration.LastSyncDate;

	public OverviewViewModel(ApplicationState applicationState)
	{
		_applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
	}

	public void Update()
	{
		Notify(nameof(BuildingAmount));
		Notify(nameof(ItemAmount));
		Notify(nameof(RecipeAmount));
		Notify(nameof(LastSyncDate));
	}

    private readonly ApplicationState _applicationState;
}
