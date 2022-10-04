namespace SatisfactoryCalculator;

public partial class App : Application
{
    public App() { }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        ServiceHost.Provider.GetRequiredService<ViewModelViewLinker>().GetViewByType<MainView>().Show();
    }
}
