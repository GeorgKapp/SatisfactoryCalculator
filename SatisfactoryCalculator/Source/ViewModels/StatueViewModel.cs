#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.ViewModels;

internal class StatueViewModel : ObservableObject
{
    private IStatue _selectedStatue;
    public IStatue SelectedStatue
    {
        get => _selectedStatue;
        set => SetProperty(ref _selectedStatue, value);
    }
    
    public StatueViewModel(ApplicationState applicationState)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
    }
    
    private readonly ApplicationState _applicationState;
}
