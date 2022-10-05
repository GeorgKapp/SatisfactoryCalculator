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
			.AddSingleton<ViewModelViewLinker>()
			.AddTransient<DataModelMappingService>()
			.AddTransient<DataModelImageCreateService>()
			.AddTransient<MessageService>()
			.AddTransient<WindowService>()
			.AddTransient<ClipBoardService>();

		return services;
	}

	private static ServiceCollection AddDomainServices(this ServiceCollection services)
	{
		services
			.AddTransient<JsonService>()
			.AddTransient<DocsParserService>();

		return services;
	}

	private static ServiceCollection AddViews(this ServiceCollection services)
	{
		services
			.AddSingleton<MainView>()
			.AddSingleton<BuildingsPage>()
			.AddSingleton<ItemsPage>()
			.AddSingleton<RecipesPage>()
			.AddSingleton<OverviewPage>()
			.AddSingleton<DataImportPage>();

		return services;
	}

	private static ServiceCollection AddViewModels(this ServiceCollection services)
	{
		services
			.AddSingleton<MainViewModel>()
			.AddSingleton<BuildingsViewModel>()
			.AddSingleton<ItemsViewModel>()
			.AddSingleton<RecipesViewModel>()
			.AddTransient<OverviewViewModel>()
			.AddSingleton<DataImportViewModel>();

		return services;
	}
}
