using System.Reflection;
using Microsoft.Extensions.Logging;
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

namespace SatisfactoryCalculator.Source.DependencyInjection;

internal static class ServiceExtensions
{
	public static void ConfigureServices(this IServiceCollection services)
	{
		var pathOptions = services.AddPathOption();
		
		services
			.AddDbContext(pathOptions)
			.AddDomainServices()
			.AddApplicationServices()
			.AddApplicationState()
			.AddViewModels()
			.AddViews();
	}

	private static PathOptions AddPathOption(this IServiceCollection services)
	{
		var dataFolder = Environment.CurrentDirectory + "\\Data";
		var imageFolder = dataFolder + "\\Images";

		services.Configure<PathOptions>(options =>
		{
			options.DataFolder = dataFolder;
			options.ImageFolder = imageFolder;
		});
		
		var pathOptionInstance = new PathOptions
		{
			DataFolder = dataFolder,
			ImageFolder = imageFolder
		};
		
		return pathOptionInstance;
	}
	
	private static IServiceCollection AddDbContext(this IServiceCollection services, PathOptions pathOptions)
	{
		services.AddDbContextFactory<ModelContext>(options =>
			options
				.UseSqlite($@"Data Source={pathOptions.DataFile};Pooling=false")
			);
		
		services.AddDbContextFactory<TempModelContext>(options =>
			options
				.UseSqlite($@"Data Source={pathOptions.TempDataFile};Pooling=false")
				.LogTo(p => Debug.WriteLine(p), LogLevel.Error)
				.EnableSensitiveDataLogging()
		);
		
		return services;
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
			.AddTransient<ModelCalculationService>()
			.AddTransient<DataModelMappingService>()
			.AddTransient<PageService>()
			.AddTransient<ClipBoardService>();

		return services;
	}

	private static IServiceCollection AddDomainServices(this IServiceCollection services)
	{
		services
			.AddTransient<JsonService>()
			.AddTransient<CalculationService>()
			.AddTransient<DocsParserService>();

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
			.AddSingleton<WeaponPage>()
			.AddSingleton<AmmunitionPage>()
			.AddSingleton<ResourcePage>()
            .AddSingleton<BuildingPage>()
            .AddSingleton<GeneratorPage>()
			.AddSingleton<MinerPage>()
			.AddSingleton<StatuePage>()
			.AddSingleton<FactoryPlannerPage>()
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
			.AddSingleton<WeaponViewModel>()
			.AddSingleton<AmmunitionViewModel>()
			.AddSingleton<ResourceViewModel>()
            .AddSingleton<BuildingViewModel>()
            .AddSingleton<GeneratorViewModel>()
			.AddSingleton<MinerViewModel>()
			.AddSingleton<StatueViewModel>()
			.AddSingleton<FactoryPlannerViewModel>()
            .AddSingleton<RecipeViewModel>()
			.AddSingleton<OverviewViewModel>()
			.AddSingleton<DataImportViewModel>();

		return services;
	}
}
