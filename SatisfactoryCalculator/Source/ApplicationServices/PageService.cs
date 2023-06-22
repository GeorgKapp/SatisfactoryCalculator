namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class PageService
{
    public static (TPage, TViewModel) FetchPageWithViewModel<TPage, TViewModel>() 
        where TPage : Page
        where TViewModel : ObservableObject
    {
        var page = ApplicationContext.Instance.ServiceProvider.GetRequiredService<TPage>();
        var viewModel = ApplicationContext.Instance.ServiceProvider.GetRequiredService<TViewModel>();

        return (page, viewModel);
    }
    
    public static TPage FetchPage<TPage>() 
        where TPage : Page
    {
        return ApplicationContext.Instance.ServiceProvider.GetRequiredService<TPage>();
    }
}