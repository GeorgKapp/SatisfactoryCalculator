namespace SatisfactoryCalculator.Source.ViewModels;

internal class GeneratorsViewModel : ObservableObject
{
    private GeneratorModel? _selectedGenerator;
    public GeneratorModel? SelectedGenerator
    {
        get => _selectedGenerator;
        set
        {
            SetProperty(ref _selectedGenerator, value);
            SelectedGeneratorFuels = value is not null
                ? new ObservableCollection<FuelModel>(_applicationState.Configuration.ReferenceDictionary[value.ClassName].FuelGenerator)
                : new();
        }
    }

    private ObservableCollection<FuelModel> _selectedGeneratorFuels = new();
    public ObservableCollection<FuelModel> SelectedGeneratorFuels
    {
        get => _selectedGeneratorFuels;
        set => SetProperty(ref _selectedGeneratorFuels, value);
    }

    public ObservableCollection<GeneratorModel> Generators => _applicationState.Configuration.Generators;

    public GeneratorsViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        Notify(nameof(Generators));
    }

    private ApplicationState _applicationState;
}