namespace SatisfactoryCalculator.Source.Base;

internal class AsyncCommand : IAsyncCommand
{
	public event EventHandler? CanExecuteChanged;

	public AsyncCommand(Func<Task> execute, bool canExecute = true)
	{
		_execute = execute;
		_canExecute = () => canExecute;
	}

	public bool CanExecute()
	{
		return !_isExecuting 
			? _canExecute?.Invoke() 
			?? true : false;
	}

	public async Task ExecuteAsync()
	{
		if (CanExecute())
		{
			try
			{
				_isExecuting = true;
				await _execute();
			}
			finally
			{
				_isExecuting = false;
			}
		}
		RaiseCanExecuteChanged();
	}

	public void RaiseCanExecuteChanged()
	{
		CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}

	bool ICommand.CanExecute(object? parameter)
	{
		return CanExecute();
	}

	void ICommand.Execute(object? parameter)
	{
		ExecuteAsync().FireAndForgetSafeAsync();
	}

    private bool _isExecuting;
    private readonly Func<Task> _execute;
    private readonly Func<bool> _canExecute;
}
