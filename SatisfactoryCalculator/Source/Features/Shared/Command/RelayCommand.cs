namespace SatisfactoryCalculator.Source.Features.Shared.Command;

internal class RelayCommand : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    // ReSharper disable once UnusedMember.Global
    public RelayCommand(Action<object?> action, Func<bool> canExecute)
    {
        _action = action;
        _canExecute = canExecute;
    }

    // ReSharper disable once HeapView.ClosureAllocation
    // ReSharper disable once UnusedMember.Global
    public RelayCommand(Action<object?> action, bool canExecute)
    {
        _action = action;
        _canExecute = () => canExecute;
    }

    public RelayCommand(Action<object?> action)
    {
        _action = action;
        _canExecute = null;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute is null || _canExecute!();
    }

    public void Execute(object? parameter)
    {
        _action(parameter);
    }

    private readonly Action<object?> _action;
    private readonly Func<bool>? _canExecute;
}
