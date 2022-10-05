﻿namespace SatisfactoryCalculator;

public partial class App : Application
{
    public App() { }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainViewModel = ServiceHost.Provider.GetRequiredService<MainViewModel>();
        var mainView = ServiceHost.Provider.GetRequiredService<MainView>();
        mainView.DataContext = mainViewModel;
        mainView.Show();
        Task.Run(() => mainViewModel.Load());
    }
}
