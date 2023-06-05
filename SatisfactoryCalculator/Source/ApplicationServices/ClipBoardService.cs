namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class ClipBoardService
{
    // ReSharper disable once HeapView.ClosureAllocation
    public static void CopyToClipboard(string content)
    {
        Application.Current.Dispatcher.Invoke(() => Clipboard.SetText(content));
    }
}
