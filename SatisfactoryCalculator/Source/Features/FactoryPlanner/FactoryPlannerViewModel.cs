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

    private Configuration _configuration = new();
    public Configuration Configuration
    {
        get => _configuration;
        set => SetProperty(ref _configuration, value);
    }
    
    private ICommand? _calculateCommand;
    public ICommand CalculateCommand => _calculateCommand ??= new SimpleCommand(Calculate);
    
    private ICommand? _saveCommand;
    public ICommand SaveCommand => _saveCommand ??= new SimpleCommand(Save);

    public FactoryPlannerViewModel(
        ApplicationState applicationState, 
        FactoryCalculationService factoryCalculationService,
        MessageBoxService messageBoxService)
    {
        _applicationState = applicationState;
        _factoryCalculationService = factoryCalculationService;
        _messageBoxService = messageBoxService;
    }

    public void Initialize()
    {
        Configuration = _applicationState.Configuration;
    }

    private void Calculate()
    {
        var calculationResult = _factoryCalculationService.CalculateFactoryConfiguration(SelectedFactoryConfiguration!);
        if(!calculationResult.IsSuccess)
            _messageBoxService.ShowWarning(calculationResult.Error!);
    }
    
    private void Save()
    {
        
    }

    private readonly ApplicationState _applicationState;
    private readonly FactoryCalculationService _factoryCalculationService;
    private readonly MessageBoxService _messageBoxService;
}
