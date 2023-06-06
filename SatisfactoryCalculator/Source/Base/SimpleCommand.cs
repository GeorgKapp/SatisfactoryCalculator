namespace SatisfactoryCalculator.Source.Base;

internal class SimpleCommand : ICommand
{
	public event EventHandler? CanExecuteChanged
	{
		add => CommandManager.RequerySuggested += value;
		remove => CommandManager.RequerySuggested -= value;
	}

	// ReSharper disable once UnusedMember.Global
	public SimpleCommand(Action action, Func<bool> canExecute)
	{
		_action = action;
		_canExecute = canExecute;
	}

	// ReSharper disable once HeapView.ClosureAllocation
	// ReSharper disable once UnusedMember.Global
	public SimpleCommand(Action action, bool canExecute)
	{
		_action = action;
		_canExecute = () => canExecute;
	}

	public SimpleCommand(Action action)
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
		_action();
	}

    private readonly Action _action;
    private readonly Func<bool>? _canExecute;
}
