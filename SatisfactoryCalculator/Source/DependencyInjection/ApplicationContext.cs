using System.Reflection;
// ReSharper disable MemberCanBeMadeStatic.Local

namespace SatisfactoryCalculator.Source.DependencyInjection;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
internal class ApplicationContext
{
	public static ApplicationContext Instance = new();
	public ServiceProvider ServiceProvider { get; private set; }
	
	public void Initialize()
	{
		InitalizeServiceProvider();
		UpdateDatabase();
		ConnectDataContextLinks();
	}

	private void InitalizeServiceProvider()
	{
		var services = new ServiceCollection();
		services.ConfigureServices();
		
		ServiceProvider = services.BuildServiceProvider(new ServiceProviderOptions
		{
			ValidateOnBuild = true,
			ValidateScopes = false
		});
	}

	private void UpdateDatabase()
	{
		using var modelContext = ServiceProvider.GetRequiredService<ModelContext>();
		modelContext.Database.Migrate();
	}

	private void ConnectDataContextLinks()
	{
		ViewDataContextLinker.LinkAll();
	}
}
