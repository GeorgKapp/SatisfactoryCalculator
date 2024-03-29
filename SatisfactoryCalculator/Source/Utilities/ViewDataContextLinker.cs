using SatisfactoryCalculator.Source.Features.Ammunition;
using SatisfactoryCalculator.Source.Features.Building;
using SatisfactoryCalculator.Source.Features.Consumable;
using SatisfactoryCalculator.Source.Features.DataImport;
using SatisfactoryCalculator.Source.Features.Equipment;
using SatisfactoryCalculator.Source.Features.FactoryPlanner;
using SatisfactoryCalculator.Source.Features.Generator;
using SatisfactoryCalculator.Source.Features.Item;
using SatisfactoryCalculator.Source.Features.Main;
using SatisfactoryCalculator.Source.Features.Shared.FilterableEntity;

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
        Link<ResourcePage, ResourceViewModel>();
        Link<BuildingPage, BuildingViewModel>();
        Link<GeneratorPage, GeneratorViewModel>();
        Link<MinerPage, MinerViewModel>();
        Link<StatuePage, StatueViewModel>();
        Link<FactoryPlannerPage, FactoryPlannerViewModel>();
        Link<RecipePage, RecipeViewModel>();
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