namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class MessageService
{
    public void ShowWarning(string message)
    {
        MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
    }

    public void ShowWarning(string message, string caption)
    {
        MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
    }

    public void ShowError(string message, string caption)
    {
        MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Hand);
    }

    public MessageBoxResult ShowQuestion(string message)
    {
        return MessageBox.Show(message, "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
    }

    public MessageBoxResult ShowQuestion(string message, string caption)
    {
        return MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
    }
}
