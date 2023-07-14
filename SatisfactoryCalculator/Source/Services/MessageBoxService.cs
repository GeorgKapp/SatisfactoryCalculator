namespace SatisfactoryCalculator.Source.Services;

internal class MessageBoxService
{
    public void ShowWarning(string message)
    {
        MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}
