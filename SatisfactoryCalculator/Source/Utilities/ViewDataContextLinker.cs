namespace SatisfactoryCalculator.Source.Utilities;

public static class ViewDataContextLinker
{
    public static void LinkAll()
    {
        Link<MainView, MainViewModel>();
        Link<FilterableEntityPage, FilterableEntityViewModel>();
        Link<ItemPage, ItemViewModel>();
        Link<EquipmentPage, EquipmentViewModel>();
        Link<ConsumablePage, ConsumableViewModel>();
        Link<BuildingPage, BuildingViewModel>();
        Link<GeneratorPage, GeneratorViewModel>();
        Link<RecipePage, RecipeViewModel>();
        Link<OverviewPage, OverviewViewModel>();
        Link<DataImportPage, DataImportViewModel>();
    }
    
    private static void Link<TView, TViewModel>() 
        where TView : FrameworkElement
        where TViewModel : ObservableObject
    {
        var view = ServiceHost.Provider.GetRequiredService<TView>();
        var viewModel = ServiceHost.Provider.GetRequiredService<TViewModel>();

        view.DataContext = viewModel;
    }
    
}