using System.Reflection;

namespace SatisfactoryCalculator.Source.DependencyInjection;

internal class ServiceHost
{
	public static ServiceHost Instance = new();
	
	public static ServiceProvider Provider { get; private set; }
	
	public void Initialize()
	{
		AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data");

		var services = new ServiceCollection();
		services.ConfigureServices();
		Provider = services.BuildServiceProvider(new ServiceProviderOptions
		{
			ValidateOnBuild = true,
			ValidateScopes = false
		});
		ViewDataContextLinker.LinkAll();
	}
}
