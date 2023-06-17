namespace SatisfactoryCalculator;

public partial class App
{
    private void OnStartup(object sender, StartupEventArgs e)
    {
        ServiceHost.Instance.Initialize();
        // ReSharper disable once HeapView.ClosureAllocation
        var mainViewModel = ServiceHost.Provider.GetRequiredService<MainViewModel>();
        var mainView = ServiceHost.Provider.GetRequiredService<MainView>();
        Task.Run(() => mainViewModel.Load());
        mainView.Show();
    }
}