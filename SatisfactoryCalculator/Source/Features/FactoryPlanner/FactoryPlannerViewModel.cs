using System.Collections.Specialized;

namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

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
    
    private ObservableCollection<IEntity> _entities = new();
    public ObservableCollection<IEntity> Entities
    {
        get => _entities;
        set => SetProperty(ref _entities, value);
    }
    
    private ICommand? _calculateCommand;
    public ICommand CalculateCommand => _calculateCommand ??= new SimpleCommand(Calculate);
    
    private ICommand? _saveCommand;
    public ICommand SaveCommand => _saveCommand ??= new SimpleCommand(Save);

    public FactoryPlannerViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState;
    }

    public void Initialize()
    {
        Entities = new(_applicationState.Configuration.Items);
    }

    private void Calculate()
    {
        
    }
    
    private void Save()
    {
        
    }

    private readonly ApplicationState _applicationState;
}
