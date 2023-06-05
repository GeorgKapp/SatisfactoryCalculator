namespace SatisfactoryCalculator.Source.DependencyInjection;

internal static class ServiceExtensions
{
	public static void ConfigureServices(this ServiceCollection services)
	{
		services
			.AddDomainServices()
			.AddApplicationServices()
			.AddApplicationState()
			.AddViewModels()
			.AddViews();
	}

	private static ServiceCollection AddApplicationState(this ServiceCollection services)
	{
		services
			.AddSingleton<ApplicationState>();

		return services;
	}

	private static ServiceCollection AddApplicationServices(this ServiceCollection services)
	{
		services
			.AddTransient<CalculationService>()
			.AddTransient<DataModelMappingService>()
			.AddTransient<MessageService>()
			.AddTransient<PageService>()
			.AddTransient<WindowService>()
			.AddTransient<ClipBoardService>();

		return services;
	}

	private static ServiceCollection AddDomainServices(this ServiceCollection services)
	{
		services
			.AddTransient<JsonService>()
			.AddTransient<DocsParserService>()
			.AddTransient<DataModelImageCreateService>();

		return services;
	}

	private static ServiceCollection AddViews(this ServiceCollection services)
	{
		services
			.AddSingleton<FilterableEntityPage>()
			.AddSingleton<MainView>()
            .AddSingleton<ItemPage>()
            .AddSingleton<BuildingPage>()
            .AddSingleton<GeneratorPage>()
            .AddSingleton<RecipePage>()
			.AddSingleton<OverviewPage>()
			.AddSingleton<DataImportPage>();

		return services;
	}

	private static ServiceCollection AddViewModels(this ServiceCollection services)
	{
		services
			.AddSingleton<FilterableEntityViewModel>()
			.AddSingleton<MainViewModel>()
            .AddSingleton<ItemViewModel>()
            .AddSingleton<BuildingViewModel>()
            .AddSingleton<GeneratorViewModel>()
            .AddSingleton<RecipeViewModel>()
			.AddTransient<OverviewViewModel>()
			.AddSingleton<DataImportViewModel>();

		return services;
	}
}
