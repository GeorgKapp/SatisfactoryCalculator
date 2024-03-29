namespace SatisfactoryCalculator.Source.Base;

internal class AsyncCommand : IAsyncCommand
{
	public event EventHandler? CanExecuteChanged;

	// ReSharper disable once HeapView.ClosureAllocation
	public AsyncCommand(Func<Task> execute, bool canExecute = true)
	{
		_execute = execute;
		_canExecute = () => canExecute;
	}

	public bool CanExecute()
	{
		return !_isExecuting && (_canExecute?.Invoke() ?? true);
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

	private void RaiseCanExecuteChanged()
	{
		CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}

	bool ICommand.CanExecute(object? parameter)
	{
		return CanExecute();
	}

	void ICommand.Execute(object? parameter)
	{
		FireAndForgetSafeAsync(ExecuteAsync());
	}
	
	public static async void FireAndForgetSafeAsync(Task task)
	{
		await task;
	}

    private bool _isExecuting;
    private readonly Func<Task> _execute;
    private readonly Func<bool>? _canExecute;
}
