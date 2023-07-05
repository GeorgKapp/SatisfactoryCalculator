namespace SatisfactoryCalculator.Source.ViewModels;

internal class FactoryPlannerViewModel : ObservableObject
{
    
    
    public FactoryPlannerViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState;
    }
    
    
    
    private readonly ApplicationState _applicationState;
}
