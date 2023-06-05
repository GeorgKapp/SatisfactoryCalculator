namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class ClipBoardService
{
    public void CopyToClipboard(string content)
    {
        Application.Current.Dispatcher.Invoke(() => Clipboard.SetText(content));
    }
}
