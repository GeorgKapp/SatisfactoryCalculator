using FactoryConfiguration = Data.Models.Implementation.FactoryConfiguration;

namespace SatisfactoryCalculator.Source.ViewModels;

internal class FactoryPlannerViewModel : ObservableObject
{
    private FactoryConfiguration? _selectedFactoryConfiguration;
    public FactoryConfiguration? SelectedFactoryConfiguration
    {
        get => _selectedFactoryConfiguration;
        set => SetProperty(ref _selectedFactoryConfiguration, value);
    }
    
    private ObservableCollection<FactoryConfiguration> _factoryConfigurations = new();
    public ObservableCollection<FactoryConfiguration> FactoryConfigurations
    {
        get => _factoryConfigurations;
        set => SetProperty(ref _factoryConfigurations, value);
    }
    
    public FactoryPlannerViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState;
    }
    
    
    
    private readonly ApplicationState _applicationState;
}
