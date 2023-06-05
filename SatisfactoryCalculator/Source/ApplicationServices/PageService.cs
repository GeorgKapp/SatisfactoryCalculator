namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class PageService
{
    public static (TPage, TViewModel) FetchPageWithViewModel<TPage, TViewModel>() 
        where TPage : Page
        where TViewModel : ObservableObject
    {
        var page = ServiceHost.Provider.GetRequiredService<TPage>();
        var viewModel = ServiceHost.Provider.GetRequiredService<TViewModel>();

        return (page, viewModel);
    }
    
    public static TPage FetchPage<TPage>() 
        where TPage : Page
    {
        return ServiceHost.Provider.GetRequiredService<TPage>();
    }
}