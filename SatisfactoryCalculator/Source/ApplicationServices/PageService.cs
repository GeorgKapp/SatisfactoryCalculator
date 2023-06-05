namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class PageService
{
    public (TPage, TViewModel) FetchPageWithViewModel<TPage, TViewModel>() 
        where TPage : Page
        where TViewModel : ObservableObject
    {
        var page = ServiceHost.Provider.GetRequiredService<TPage>();
        var viewModel = ServiceHost.Provider.GetRequiredService<TViewModel>();
        page.DataContext = viewModel;
        return (page, viewModel);
    }
    
    public TPage FetchPage<TPage, TViewModel>() 
        where TPage : Page
        where TViewModel : ObservableObject
    {
        var page = ServiceHost.Provider.GetRequiredService<TPage>();
        page.DataContext = ServiceHost.Provider.GetRequiredService<TViewModel>();
        return page;
    }
}