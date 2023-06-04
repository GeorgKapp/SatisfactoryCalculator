namespace SatisfactoryCalculator.Source.Base;

internal class ViewModelViewLinker
{
	private Dictionary<Type, Type> ViewToViewModelDictionary = new Dictionary<Type, Type>();

	public ViewModelViewLinker()
	{
		AddLink<MainView, MainViewModel>();
		AddLink<ItemPage, ItemViewModel>();
        AddLink<BuildingPage, BuildingViewModel>();
        AddLink<GeneratorPage, GeneratorViewModel>();
        AddLink<RecipePage, RecipeViewModel>();
		AddLink<OverviewPage, OverviewViewModel>();
		AddLink<DataImportPage, DataImportViewModel>();
	}

	public T GetViewByType<T>() where T : Window
	{
		return GetFrameworkElementByType<T>();
	}

	public T GetPageByType<T>() where T : Page
	{
		return GetFrameworkElementByType<T>();
	}

	private T GetFrameworkElementByType<T>() where T : FrameworkElement
	{
		var frameworkElement = ServiceHost.Provider.GetRequiredService<T>();
		frameworkElement.DataContext = ServiceHost.Provider.GetRequiredService(ViewToViewModelDictionary[typeof(T)]);
		return frameworkElement;
	}

	private void AddLink<THost, TViewModel>()
	{
		ViewToViewModelDictionary.Add(typeof(THost), typeof(TViewModel));
	}
}
