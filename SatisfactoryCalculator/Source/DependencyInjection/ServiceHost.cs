namespace SatisfactoryCalculator.Source.DependencyInjection;

internal static class ServiceHost
{
	public static ServiceProvider Provider { get; private set; }

	static ServiceHost()
	{
		ServiceCollection services = new ServiceCollection();
		services.ConfigureServices();
		Provider = services.BuildServiceProvider(new ServiceProviderOptions
		{
			ValidateOnBuild = true,
			ValidateScopes = false
		});
	}
}
