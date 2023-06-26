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
        Link<WeaponPage, WeaponViewModel>();
        Link<AmmunitionPage, AmmunitionViewModel>();
        Link<BuildingPage, BuildingViewModel>();
        Link<GeneratorPage, GeneratorViewModel>();
        Link<RecipePage, RecipeViewModel>();
        Link<ResourcePage, ResourceViewModel>();
        Link<OverviewPage, OverviewViewModel>();
        Link<DataImportPage, DataImportViewModel>();
    }
    
    private static void Link<TView, TViewModel>() 
        where TView : FrameworkElement
        where TViewModel : ObservableObject
    {
        var view = ApplicationContext.Instance.ServiceProvider.GetRequiredService<TView>();
        var viewModel = ApplicationContext.Instance.ServiceProvider.GetRequiredService<TViewModel>();

        view.DataContext = viewModel;
    }
    
}