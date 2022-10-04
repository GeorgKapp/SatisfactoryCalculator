namespace SatisfactoryCalculator.Source.Base;

internal interface IAsyncCommand : ICommand
{
	Task ExecuteAsync();
	bool CanExecute();
}
