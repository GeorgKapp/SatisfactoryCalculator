namespace SatisfactoryCalculator.Source.DependencyInjection;

internal static class ServiceExtensions
{
	public static void ConfigureServices(this IServiceCollection services)
	{
		services
			.AddDomainServices()
			.AddApplicationServices()
			.AddApplicationState()
			.AddViewModels()
			.AddViews();
	}

	private static IServiceCollection AddApplicationState(this IServiceCollection services)
	{
		services
			.AddSingleton<ApplicationState>();

		return services;
	}

	private static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services
			.AddTransient<CalculationService>()
			.AddTransient<DataModelMappingService>()
			.AddTransient<PageService>()
			.AddTransient<ClipBoardService>();

		return services;
	}

	private static IServiceCollection AddDomainServices(this IServiceCollection services)
	{
		services
			.AddTransient<JsonService>()
			.AddTransient<DocsParserService>()
			.AddTransient<DataModelImageCreateService>();

		return services;
	}

	private static void AddViews(this IServiceCollection services)
	{
		services
			.AddSingleton<FilterableEntityPage>()
			.AddSingleton<MainView>()
            .AddSingleton<ItemPage>()
			.AddSingleton<EquipmentPage>()
			.AddSingleton<ConsumablePage>()
            .AddSingleton<BuildingPage>()
            .AddSingleton<GeneratorPage>()
            .AddSingleton<RecipePage>()
			.AddSingleton<OverviewPage>()
			.AddSingleton<DataImportPage>();
	}

	private static IServiceCollection AddViewModels(this IServiceCollection services)
	{
		services
			.AddSingleton<FilterableEntityViewModel>()
			.AddSingleton<MainViewModel>()
            .AddSingleton<ItemViewModel>()
			.AddSingleton<EquipmentViewModel>()
			.AddSingleton<ConsumableViewModel>()
            .AddSingleton<BuildingViewModel>()
            .AddSingleton<GeneratorViewModel>()
            .AddSingleton<RecipeViewModel>()
			.AddSingleton<OverviewViewModel>()
			.AddSingleton<DataImportViewModel>();

		return services;
	}
}
