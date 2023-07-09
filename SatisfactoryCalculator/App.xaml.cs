using SatisfactoryCalculator.Source.Features.Main;

namespace SatisfactoryCalculator;

public partial class App
{
    private void OnStartup(object sender, StartupEventArgs e)
    {
        ApplicationContext.Instance.Initialize();
        
        // ReSharper disable once HeapView.ClosureAllocation
        var mainViewModel = ApplicationContext.Instance.ServiceProvider.GetRequiredService<MainViewModel>();
        var mainView = ApplicationContext.Instance.ServiceProvider.GetRequiredService<MainView>();
        Task.Run(() => mainViewModel.Load());
        mainView.Show();
    }
}