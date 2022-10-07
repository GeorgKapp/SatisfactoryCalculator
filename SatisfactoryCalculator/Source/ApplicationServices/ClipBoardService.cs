namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class ClipBoardService
{
    public void CopyToClipboard(string content)
    {
        App.Current.Dispatcher.Invoke(() => Clipboard.SetText(content));
    }
}
