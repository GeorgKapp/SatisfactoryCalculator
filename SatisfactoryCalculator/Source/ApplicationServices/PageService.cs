namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class PageService
{
    public (TPage, TViewModel) FetchPageWithViewModel<TPage, TViewModel>() 
        where TPage : Page
        where TViewModel : ObservableObject
    {
        var page = ServiceHost.Provider.GetRequiredService<TPage>();
        var viewModel = ServiceHost.Provider.GetRequiredService<TViewModel>();

        return (page, viewModel);
    }
    
    public TPage FetchPage<TPage>() 
        where TPage : Page
    {
        return ServiceHost.Provider.GetRequiredService<TPage>();
    }
}