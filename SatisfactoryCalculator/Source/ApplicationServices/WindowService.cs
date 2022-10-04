namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class WindowService
{
	public void ShowWindow<TWindow, TViewModel>(TViewModel viewModel) where TWindow : Window
	{
		var window = ServiceHost.Provider.GetRequiredService<TWindow>();
		window.DataContext = viewModel;
		window.Show();
	}

	public void ShowDialogWindow<TWindow, TViewModel>(TViewModel viewModel) where TWindow : Window
	{
		var window = ServiceHost.Provider.GetRequiredService<TWindow>();
		window.DataContext = viewModel;
		window.ShowDialog();
	}
}
